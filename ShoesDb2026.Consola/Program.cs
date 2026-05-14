using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using ShoesDb2026.Entities;
using ShoesDb2026.IoC;
using ShoesDb2026.Service.DTOs.Brand;
using ShoesDb2026.Service.DTOs.Sport;
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
                Console.WriteLine("4. Shoes");
                Console.WriteLine("0. Exit");

                Console.Write("Select option: ");

                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        BrandsMenu();
                        break;
                    case "2":
                        SportsMenu();
                        break;
                    case "3":
                        //SizesMenu();
                        break;
                    case "4":
                        //ShoesMenu();
                        break;
                    case "0":
                        return;
                }

            } while (true);
        }

        private static void SportsMenu()
        {
            using (var scope = provider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ISportService>();

                do
                {
                    Console.Clear();

                    Console.WriteLine("SPORTS SECTION");
                    Console.WriteLine("1 - List Sports");
                    Console.WriteLine("2 - Add Sport");
                    Console.WriteLine("3 - Delete Sport");
                    Console.WriteLine("4 - Update Sport");
                    Console.WriteLine("0 - Back");

                    Console.Write("Select option: ");

                    var op = Console.ReadLine();

                    switch (op)
                    {
                        case "1":
                            ListSports(service);
                            break;

                        case "2":
                            AddSport(service);
                            break;

                        case "3":
                            DeleteSport(service);
                            break;

                        case "4":
                            UpdateSport(service);
                            break;

                        case "0":
                            return;
                    }

                } while (true);
            }
        }

        private static void UpdateSport(ISportService service)
        {
            Console.Clear();
            Console.WriteLine("UPDATE SPORT:");

            ShowSports(service);

            Console.WriteLine("SELECT ID SPORT TO UPDATE:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }

            var sportUpdate = service.GetForUpdate(id);
            if (sportUpdate.IsFailure)
            {
                ShowErrors(sportUpdate.Errors);
                return;
            }

            var sport = sportUpdate.Value!;
            Console.WriteLine("NEW SPORT NAME: ");
            var newSportName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSportName))
            {
                sport.SportName = newSportName;
            }
            var result = service.Update(sport);
            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("SPORT UPDATED SUCCESSFULLY!");
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadLine();
        }

        private static void DeleteSport(ISportService service)
        {
            Console.Clear();
            Console.WriteLine("DELETE SPORT:");
            ShowSports(service);

            Console.WriteLine("SELECT ID SPORT TO DELETE:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("ARE YOU SURE TO DELETE SPORT? (Y/N)");
            var confirm = Console.ReadLine();
            if (confirm?.ToUpper() != "Y")
            {
                Console.WriteLine("DELETE CANCELLED!");
                Console.ReadLine();
                return;
            }

            var sportResult = service.Delete(id);

            if (sportResult.IsFailure)
            {
                ShowErrors(sportResult.Errors);
            }
            else
            {
                Console.WriteLine("SPORT DELETED SUCCESSFULLY!");
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadLine();
        }

        private static void AddSport(ISportService service)
        {
            Console.Clear();
            Console.WriteLine("ADD SPORT:");

            var dto = new SportCreateDto();

            Console.WriteLine("SPORT NAME: ");
            dto.SportName = Console.ReadLine() ?? "";

            var result = service.Add(dto);

            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("SPORT ADDED SUCCESSFULLY!");
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadKey();
        }

        private static void ListSports(ISportService service)
        {
            Console.Clear();
            Console.WriteLine("LIST OF SPORTS:");
            ShowSports(service);
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadKey();
        }

        private static void ShowSports(ISportService service)
        {
            var sportsResult = service.GetAll();
            if (sportsResult.IsFailure)
            {
                ShowErrors(sportsResult.Errors);
                return;
            }

            var sports = sportsResult.Value;
            foreach (var sport in sports!)
            {
                Console.WriteLine($"ID: {sport.SportId} -- Name: {sport.SportName}");
            }
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
                            UpdateBrand(service);
                            break;

                        case "0":
                            return;
                    }

                } while (true);
            }
        }

        private static void UpdateBrand(IBrandService service)
        {
            Console.Clear();
            Console.WriteLine("UPDATE BRAND:");

            ShowBrands(service);

            Console.WriteLine("SELECT ID BRAND TO UPDATE:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }

            var brandUpdate = service.GetForUpdate(id);
            if (brandUpdate.IsFailure)
            {
                ShowErrors(brandUpdate.Errors);
                return;
            }

            var brand = brandUpdate.Value!;
            Console.WriteLine("NEW BRAND NAME: ");
            var newBrandName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newBrandName))
            {
                brand.BrandName = newBrandName;
            }

            Console.WriteLine("NEW IMAGE URL: ");
            var newImageUrl = Console.ReadLine();
            if (!string.IsNullOrEmpty(newImageUrl))
            {
                brand.ImageUrl = newImageUrl;
            }

            var result = service.Update(brand);
            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("BRAND UPDATED SUCCESSFULLY!");
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadLine();
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
