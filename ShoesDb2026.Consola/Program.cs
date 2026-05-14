using Microsoft.Extensions.DependencyInjection;
using ShoesDb2026.IoC;
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
                            //AddBrand(service);
                            break;

                        case "3":
                            //DeleteBrand(service);
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
