using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using ShoesDb2026.Entities;
using ShoesDb2026.IoC;
using ShoesDb2026.Service.DTOs.Brand;
using ShoesDb2026.Service.Interfaces;

namespace ShoesDb2026.Consola
{
    internal class Program
    {
        static IServiceProvider provider= DependencyInyectionContainer.Configure();
        static void Main(string[] args)
        {
            do
            {
                Console.Clear();

                Console.WriteLine("SHOES MANAGEMENT:");
                Console.WriteLine("1. Brands");
                Console.WriteLine("2. Sports");
                Console.WriteLine("3. Size");
                Console.WriteLine("4. Sport Shoes");
                Console.WriteLine("0. Exit");

                Console.Write("Select option: ");

                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        BrandsMenu();
                        break;
                    case "2":
                        //SportsMenu();
                        break;
                    case "3":
                        //SizeMenu();
                        break;
                    case "4":
                        //SportShoesMenu();
                        break;
                    case "0":
                        return;
                }

            } while (true);
        }

        private static void BrandsMenu()
        {
            using (var scope = provider.CreateScope()) 
            { 
                var service=scope.ServiceProvider.GetRequiredService<IBrandService>();

                do
                {
                    Console.Clear();

                    Console.WriteLine("BRANDS SECTION");
                    Console.WriteLine("1 - List Brands");
                    Console.WriteLine("2 - Add Brand");
                    Console.WriteLine("3 - Delete Brand");
                    Console.WriteLine("4 - Update Brand");
                    Console.WriteLine("0 - Back");

                    Console.Write("Select option: ");

                    var op = Console.ReadLine();

                    switch (op)
                    {
                        case "1":
                            ListBrands(service);
                            break;

                        case "2":
                            AddBrand(service);
                            break;

                        case "3":
                            DeleteBrand(service);
                            break;

                        case "4":
                            //UpdateBrand(service);
                            break;

                        case "0":
                            return;
                    }

                } while (true);
            }
        }

        private static void DeleteBrand(IBrandService service)
        {
            Console.Clear();
            Console.WriteLine("DELETE BRAND:");
            ShowBrands(service);

            Console.WriteLine("SELECT ID BRAND TO DELETE:");
            if(!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("INVALID ID!");
                //Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("ARE YOU SURE TO DELETE BRAND? (Y/N)");
            var confirm = Console.ReadLine();
            if (confirm?.ToUpper() != "Y")
            {
                Console.WriteLine("DELETE CANCELLED!");
                Console.ReadLine();
                return;
            }

            var brandResult=service.Delete(id);

            if (brandResult.IsFailure)
            {
                ShowErrors(brandResult.Errors);
            }
            else
            {
                Console.WriteLine("BRAND DELETED SUCCESSFULLY!");
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadLine();
            //var brandsResult = service.GetAll();
            //if (brandsResult.IsFailure)
            //{
            //    ShowErrors(brandsResult.Errors);
            //    return;
            //}
            //foreach (var brand in brandsResult.Value!)
            //{
            //    Console.WriteLine($"ID: {brand.BrandId} -- Name: {brand.BrandName}");
            //}

        }

        private static void AddBrand(IBrandService service)
        {
            Console.Clear();
            Console.WriteLine("ADD BRAND:");

            var dto = new BrandCreateDto();

            Console.WriteLine("BRAND NAME: ");
            dto.BrandName = Console.ReadLine()??"";
            Console.WriteLine("IMAGE URL: ");
            dto.ImageUrl = Console.ReadLine();

            var result = service.Add(dto);

            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else 
            {
                Console.WriteLine();
                Console.WriteLine("BRAND ADDED SUCCESSFULLY!");
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadKey();
        }

        private static void ListBrands(IBrandService service)
        {
            Console.Clear();
            Console.WriteLine("LIST OF BRANDS:");
            ShowBrands(service);
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadKey();
        }

        private static void ShowBrands(IBrandService service)
        {
            var brandsResult = service.GetAll();
            if (brandsResult.IsFailure)
            {
                ShowErrors(brandsResult.Errors);
                return;
            }

            var brands = brandsResult.Value;
            foreach (var brand in brands!)
            {
                Console.WriteLine($"ID: {brand.BrandId} -- Name: {brand.BrandName}");
            }
        }

        private static void ShowErrors(List<string> errors)
        {
            foreach (var error in errors) 
            {
                Console.WriteLine(error);
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadKey();
        }
    }
}
