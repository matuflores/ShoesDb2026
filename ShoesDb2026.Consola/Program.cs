using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using ShoesDb2026.Entities;
using ShoesDb2026.IoC;
using ShoesDb2026.Service.Common;
using ShoesDb2026.Service.DTOs.Brand;
using ShoesDb2026.Service.DTOs.Shoe;
using ShoesDb2026.Service.DTOs.Size;
using ShoesDb2026.Service.DTOs.Sport;
using ShoesDb2026.Service.Interfaces;
using ShoesDb2026.Service.Services;
using System.Net.WebSockets;

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
                Console.WriteLine("1. BRANDS");
                Console.WriteLine("2. SPORTS");
                Console.WriteLine("3. SIZES");
                Console.WriteLine("4. SHOES");
                Console.WriteLine("0. EXIT");

                Console.Write("SELECT OPTION: ");

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
                        SizesMenu();
                        break;
                    case "4":
                        ShoesMenu();
                        break;
                    case "0":
                        return;
                }

            } while (true);
        }

        private static void ShoesMenu()
        {
            using (var scope = provider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<IShoeService>();
                var sizeService = scope.ServiceProvider.GetRequiredService<ISizeService>();
                var sportService = scope.ServiceProvider.GetRequiredService<ISportService>();
                var brandService = scope.ServiceProvider.GetRequiredService<IBrandService>();


                do
                {
                    Console.Clear();

                    Console.WriteLine("SHOES SECTION");
                    Console.WriteLine("1 - LIST SHOES");
                    Console.WriteLine("2 - ADD SHOE");
                    Console.WriteLine("3 - DELETE SHOE");
                    Console.WriteLine("4 - UPDATE SHOE");
                    Console.WriteLine("5 - DETAILS SHOE");
                    Console.WriteLine("6 - FILTERS");
                    Console.WriteLine("0 - BACK");

                    Console.Write("SELECT OPTION: ");

                    var op = Console.ReadLine();

                    switch (op)
                    {
                        case "1":
                            ListShoes(service);
                            break;

                        case "2":
                            AddShoe(service, sizeService, sportService, brandService);
                            break;

                        case "3":
                            DeleteShoe(service);
                            break;

                        case "4":
                            UpdateShoe(service, sizeService, sportService, brandService);
                            break;
                        case "5":
                            DetailsShoes(service);
                            break;
                        case "6":
                            FiltersShoes(service, sizeService, sportService, brandService);
                            break;
                        case "0":
                            return;
                    }

                } while (true);
            }
        }

        private static void FiltersShoes(IShoeService service, ISizeService sizeService, ISportService sportService, IBrandService brandService)
        {
            do
            {
                Console.Clear();

                Console.WriteLine("SELECT FILTERS:");
                Console.WriteLine("1 - FILTER BY BRAND");
                Console.WriteLine("2 - FILTER BY SPORT");
                Console.WriteLine("3 - FILTER BY SIZE");
                Console.WriteLine();
                Console.WriteLine("SELECT ORDER:");
                Console.WriteLine("4 - ORDER BY MODEL");
                Console.WriteLine("5 - ORDER BY PRICE");
                Console.WriteLine("6 - ORDER BY BRAND");
                Console.WriteLine();
                Console.WriteLine("0 - BACK");

                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        FilterByBrand(service, brandService);
                        break;

                    case "2":
                        FilterBySport(service, sportService);
                        break;

                    case "3":
                        FilterBySize(service, sizeService);
                        break;

                    case "4":
                        OrderByModel(service);
                        break;

                    case "5":
                        OrderByPrice(service);
                        break;

                    case "6":
                        OrderByBrand(service);
                        break;

                    case "0":
                        return;
                }

            } while (true);
        }

        private static void OrderByBrand(IShoeService service)
        {
            Console.Clear();
            Console.WriteLine("ORDER SHOES BY BRAND:");
            Console.WriteLine();

            var result = service.OrderByBrand();

            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
                return;
            }
            ShowShoesFilter(result.Value!);
            Console.ReadLine();
        }

        private static void OrderByPrice(IShoeService service)
        {
            Console.Clear();
            Console.WriteLine("ORDER SHOES BY PRICE:");
            Console.WriteLine();

            var result = service.OrderByPrice();

            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
                return;
            }
            ShowShoesFilter(result.Value!);
            Console.ReadLine();
        }

        private static void OrderByModel(IShoeService service)
        {
            Console.Clear();
            Console.WriteLine("ORDER SHOES BY MODEL:");
            Console.WriteLine();

            var result = service.OrderByModel();

            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
                return;
            }
            ShowShoesFilter(result.Value!);
            Console.ReadLine();
        }

        private static void FilterBySize(IShoeService service, ISizeService sizeService)
        {
            //Console.Clear();
            //Console.WriteLine("FILTER SHOES BY SIZE:");
            //Console.WriteLine();
            //ShowSizes(sizeService);

            //Console.Write("SELECT SIZE ID (0 TO QUIT):");
            //int sizeId = int.Parse(Console.ReadLine()!);
            //if (sizeId == 0) return;
            //var result = service.GetBySize(sizeId);
            //if (result.IsFailure)
            //{
            //    ShowErrors(result.Errors);
            //    return;
            //}
            //ShowShoesFilter(result.Value!);
            //Console.ReadLine();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("FILTER SHOES BY SPORT:");
                Console.WriteLine();
                ShowSizes(sizeService);

                Console.Write("SELECT SIZE ID (0 TO QUIT):");
                string selection = Console.ReadLine()!;
                if (!int.TryParse(selection, out int sizeId))
                {
                    Console.WriteLine("INVALID ID! PLEASE ENTER A VALID NUMBER.");
                    Console.WriteLine("PRESS ANY KEY TO RESTART...");
                    Console.ReadKey();
                    continue;
                }

                if (sizeId == 0) break;

                var result = service.GetBySize(sizeId);

                if (result.IsFailure)
                {
                    ShowErrors(result.Errors);
                    continue;
                }
                Console.Clear();
                ShowShoesFilter(result.Value!);
                Console.WriteLine("DO YOU WANT TO MAKE NEW FILTER? (Y/N)");
                var response = Console.ReadLine();
                if (response?.ToUpper() != "Y")
                {
                    break;
                }
            }
            Console.WriteLine("FILTERING FINISHED.");
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadKey();
        }

        private static void FilterBySport(IShoeService service, ISportService sportService)
        {
            //Console.Clear();
            //Console.WriteLine("FILTER SHOES BY SPORT:");
            //Console.WriteLine();
            //ShowSports(sportService);

            //Console.Write("SELECT SPORT ID (0 TO QUIT):");
            //int sportId = int.Parse(Console.ReadLine()!);
            //if (sportId == 0) return;
            //var result = service.GetBySport(sportId);
            //if (result.IsFailure)
            //{
            //    ShowErrors(result.Errors);
            //    return;
            //}
            //ShowShoesFilter(result.Value!);
            //Console.ReadLine();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("FILTER SHOES BY SPORT:");
                Console.WriteLine();
                ShowSports(sportService);

                Console.Write("SELECT SPORT ID (0 TO QUIT):");
                string selection = Console.ReadLine()!;
                if (!int.TryParse(selection, out int sportId))
                {
                    Console.WriteLine("INVALID ID! PLEASE ENTER A VALID NUMBER.");
                    Console.WriteLine("PRESS ANY KEY TO RESTART...");
                    Console.ReadKey();
                    continue;
                }

                if (sportId == 0) break;

                var result = service.GetBySport(sportId);

                if (result.IsFailure)
                {
                    ShowErrors(result.Errors);
                    continue;
                }
                Console.Clear();
                ShowShoesFilter(result.Value!);
                Console.WriteLine("DO YOU WANT TO MAKE NEW FILTER? (Y/N)");
                var response = Console.ReadLine();
                if (response?.ToUpper() != "Y")
                {
                    break;
                }
            }
            Console.WriteLine("FILTERING FINISHED.");
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadKey();
        }

        private static void FilterByBrand(IShoeService service, IBrandService brandService)
        {
            //FUNCIONA DE ACA------------------------------------------------------------------------------
            //Console.Clear();
            //Console.WriteLine("FILTER SHOES BY BRAND:");
            //Console.WriteLine();
            //ShowBrands(brandService);

            //Console.Write("SELECT BRAND ID (0 TO QUIT):");
            //int brandId = int.Parse(Console.ReadLine()!);
            //if (brandId == 0) return;
            //var result = service.GetByBrand(brandId);
            //if(result.IsFailure)
            //{
            //    ShowErrors(result.Errors);
            //    return;
            //}
            //ShowShoesFilter(result.Value!);
            //Console.ReadLine();
            //HASTA ACA-------------------------------------------------------------------------------------
            

            while (true)
            {
                Console.Clear();
                Console.WriteLine("FILTER SHOES BY BRAND:");
                Console.WriteLine();
                ShowBrands(brandService);

                Console.Write("SELECT BRAND ID (0 TO QUIT):");
                string selection =Console.ReadLine()!;
                if(!int.TryParse(selection, out int brandId))
                {
                    Console.WriteLine("INVALID ID! PLEASE ENTER A VALID NUMBER.");
                    Console.WriteLine("PRESS ANY KEY TO RESTART...");
                    Console.ReadKey();
                    continue;
                }

                if (brandId == 0) break;

                var result = service.GetByBrand(brandId);

                if (result.IsFailure)
                {
                    ShowErrors(result.Errors);
                    continue;
                }
                Console.Clear();
                ShowShoesFilter(result.Value!);
                Console.WriteLine("DO YOU WANT TO MAKE NEW FILTER? (Y/N)");
                var response = Console.ReadLine();
                if (response?.ToUpper() != "Y")
                {
                    break;
                }
            }
            Console.WriteLine("FILTERING FINISHED.");
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadKey();
        }

        private static void ShowShoesFilter(List<ShoeListDto> shoeListDtos)
        {
            
            foreach (var shoe in shoeListDtos)
            {
                Console.WriteLine($"{"Id:",-10}{shoe.ShoeId}");
                Console.WriteLine($"{"Model:",-10}{shoe.Model}");
                Console.WriteLine($"{"Brand:",-10}{shoe.BrandName}");
                Console.WriteLine($"{"Sport:",-10}{shoe.SportName}");
                Console.WriteLine($"{"Size:",-10}{shoe.SizeNumber}");
                Console.WriteLine($"{"Price:",-10}{shoe.Price:C}" );
                Console.WriteLine("-----------------------------------------------------------------");
            }
        }

        private static void DetailsShoes(IShoeService service)
        {
            Console.Clear();
            ShowShoes(service);
            Console.WriteLine();
            while (true)
            {
                Console.Write("SELECT ID SHOE TO VIEW DETAILS (0 TO QUIT): ");
                int idSelect = int.Parse(Console.ReadLine()!);
                var result = service.GetDetails(idSelect);
                if (idSelect == 0) return;
                if (result.IsFailure)
                {
                    ShowErrors(result.Errors);
                    return;
                }

                var shoeDetails = result.Value!;
                Console.WriteLine("------------------------------------------------------------------------------");
                Console.WriteLine($"ID: {shoeDetails.ShoeId}");
                Console.WriteLine($"MODEL: {shoeDetails.Model}");
                Console.WriteLine($"DESCRIPTION: {shoeDetails.Description}");
                Console.WriteLine($"PRICE: {shoeDetails.Price:C}");
                Console.WriteLine($"BRAND: {shoeDetails.BrandName}");
                Console.WriteLine($"SPORT: {shoeDetails.SportName}");
                Console.WriteLine($"GENDER: {shoeDetails.GenreName}");
                Console.WriteLine($"SIZE: {shoeDetails.SizeNumber}");
                Console.WriteLine("------------------------------------------------------------------------------");

                Console.WriteLine("DO YOU WANT TO MAKE NEW SELECTION? (Y/N)");
                var response = Console.ReadLine();
                if (response?.ToUpper() != "Y")
                {
                    break;
                }
            }
            Console.ReadLine();
        }

        private static void UpdateShoe(IShoeService service, ISizeService sizeService, ISportService sportService, IBrandService brandService)
        {
            Console.Clear();
            Console.WriteLine("UPDATE SHOE:");
            ShowShoes(service);
            Console.WriteLine();
            Console.Write("SELECT ID SHOE TO UPDATE:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }

            var shoeUpdate = service.GetForUpdate(id);
            if (shoeUpdate.IsFailure)
            {
                ShowErrors(shoeUpdate.Errors);
                return;
            }
            var dto = shoeUpdate.Value!;
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine($"MODEL CURRENT: {dto.Model} ");
            Console.Write("ENTER NEW MODEL: ");
            var model=Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(model))
            {
                dto.Model = model;
            }
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine($"GENDER CURRENT: {dto.GenreId}");
            Console.WriteLine("GENDERS AVAILABLE:");
            var genreResult = service.GetGenres();
            if (genreResult.IsSuccess)
            {
                foreach (var genre in genreResult.Value!)
                {
                    Console.WriteLine($"ID: {genre.GenreId} -- Name: {genre.GenreName}");
                }
            }
            Console.Write("SELECT NEW GENDER ID:");
            if (!int.TryParse(Console.ReadLine(), out int genreId))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }
            dto.GenreId = genreId;
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine($"BRAND ID CURRENT: {dto.BrandId}");
            Console.WriteLine("BRANDS AVAILABLE:");
            var brandResult = brandService.GetAll();
            if (brandResult.IsSuccess)
            {
                foreach (var brand in brandResult.Value!)
                {
                    Console.WriteLine($"ID: {brand.BrandId} -- Name: {brand.BrandName}");
                }
            }
            Console.Write("SELECT NEW BRAND ID:");
            if (!int.TryParse(Console.ReadLine(), out int brandId))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }
            dto.BrandId = brandId;
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine($"SPORT ID CURRENT: {dto.SportId}");
            var sportResult = sportService.GetAll();
            if (sportResult.IsSuccess)
            {
                foreach (var sport in sportResult.Value!)
                {
                    Console.WriteLine($"ID: {sport.SportId} -- Name: {sport.SportName}");
                }
            }
            Console.Write("SELECT NEW SPORT ID:");
            if (!int.TryParse(Console.ReadLine(), out int sportId))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }
            dto.SportId = sportId;
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine($"DESCRIPTION CURRENT: {dto.Description}");
            Console.Write("ENTER NEW DESCRIPTION: ");
            var description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
            {
                dto.Description = description;
            }
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine($"PRICE CURRENT: {dto.Price}");
            Console.Write("ENTER NEW PRICE: ");
            var priceInput = Console.ReadLine();
            if (decimal.TryParse(priceInput, out decimal price))
            {
                dto.Price = price;
            }
            Console.WriteLine("------------------------------------------------------------------------------");
            var result = service.Update(dto);
            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("SHOE UPDATED SUCCESSFULLY!");
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadLine();
        }

        private static void DeleteShoe(IShoeService service)
        {
            Console.Clear();
            Console.WriteLine("DELETE SHOE:");
            ShowShoes(service);

            Console.WriteLine("SELECT ID SHOE TO DELETE:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("ARE YOU SURE TO DELETE SHOE? (Y/N)");
            var confirm = Console.ReadLine();
            if (confirm?.ToUpper() != "Y")
            {
                Console.WriteLine("DELETE CANCELLED!");
                Console.ReadLine();
                return;
            }

            var sizeResult = service.Delete(id);

            if (sizeResult.IsFailure)
            {
                ShowErrors(sizeResult.Errors);
            }
            else
            {
                Console.WriteLine("SHOE DELETED SUCCESSFULLY!");
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadLine();
        }

        private static void AddShoe(IShoeService service, ISizeService sizeService, ISportService sportService, IBrandService brandService)
        {
            Console.Clear();
            Console.WriteLine("ADD NEW SHOE:");

            var newShoe = new ShoeCreateDto();

            Console.WriteLine("MODEL:");
            newShoe.Model = Console.ReadLine()!;

            Console.WriteLine("SELECT GENDER TO LIST:");
            var genreResult = service.GetGenres();
            if (genreResult.IsSuccess) 
            {
                foreach (var genre in genreResult.Value!)
                {
                    Console.WriteLine($"ID: {genre.GenreId} -- Name: {genre.GenreName}");
                }
            }
            Console.WriteLine("SELECT GENDER ID:");
            if (!int.TryParse(Console.ReadLine(), out int genreId))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }
            newShoe.GenreId = genreId;

            Console.WriteLine("SELECT BRAND TO LIST:");
            var brandResult = brandService.GetAll();
            if (brandResult.IsSuccess)
            {
                foreach (var brand in brandResult.Value!)
                {
                    Console.WriteLine($"ID: {brand.BrandId} -- Name: {brand.BrandName}");
                }
            }
            Console.WriteLine("SELECT BRAND ID:");
            if (!int.TryParse(Console.ReadLine(), out int brandId))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }
            newShoe.BrandId = brandId;

            Console.WriteLine("SELECT SPORT TO LIST:");
            var sportResult = sportService.GetAll();
            if (sportResult.IsSuccess)
            {
                foreach (var sport in sportResult.Value!)
                {
                    Console.WriteLine($"ID: {sport.SportId} -- Name: {sport.SportName}");
                }
            }
            Console.WriteLine("SELECT SPORT ID:");
            if (!int.TryParse(Console.ReadLine(), out int sportId))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }
            newShoe.SportId = sportId;

            Console.WriteLine("SELECT SIZE TO LIST:");
            var sizeResult = sizeService.GetAll();
            if (sizeResult.IsSuccess)
            {
                foreach (var size in sizeResult.Value!)
                {
                    Console.WriteLine($"ID: {size.SizeId} -- Number: {size.SizeNumber}");
                }
            }
            Console.WriteLine("SELECT SIZE ID:");
            if (!int.TryParse(Console.ReadLine(), out int sizeId))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }
            newShoe.SizeId = sizeId;

            Console.WriteLine("ADD THE PRICE:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("INVALID PRICE!");
                Console.ReadLine();
                return;
            }
            newShoe.Price = price;

            Console.WriteLine("DESCRIPTION:");
            newShoe.Description = Console.ReadLine()!;

            var result = service.Add(newShoe);
            if (result.IsFailure)
            {
                ShowErrors(result.Errors);

            }
            else
            {
                Console.WriteLine("SHOE ADDED SUCCESSFULLY!");
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadLine();
        }

        private static void ListShoes(IShoeService service)
        {
            Console.Clear();
            Console.WriteLine("LIST OF SHOES:");
            ShowShoes(service);
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadLine();
        }

        private static void ShowShoes(IShoeService service)
        {
            var shoeResult = service.GetAll();
            if (shoeResult.IsFailure)
            {
                ShowErrors(shoeResult.Errors);
                return;
            }

            var shoes = shoeResult.Value;
            foreach (var shoe in shoes!)
            {
                Console.WriteLine($"ID: {shoe.ShoeId} -- Model: {shoe.Model} -- Brand: {shoe.BrandName} -- Sport: {shoe.SportName} -- Size: {shoe.SizeNumber} -- Price: {shoe.Price} ");
            }
        }

        private static void SizesMenu()
        {
            using (var scope = provider.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<ISizeService>();

                do
                {
                    Console.Clear();

                    Console.WriteLine("SIZES SECTION");
                    Console.WriteLine("1 - List Sizes");
                    Console.WriteLine("2 - Add Size");
                    Console.WriteLine("3 - Delete Size");
                    Console.WriteLine("4 - Update Size");
                    Console.WriteLine("0 - Back");

                    Console.Write("Select option: ");

                    var op = Console.ReadLine();

                    switch (op)
                    {
                        case "1":
                            ListSizes(service);
                            break;

                        case "2":
                            AddSize(service);
                            break;

                        case "3":
                            DeleteSize(service);
                            break;

                        case "4":
                            UpdateSize(service);
                            break;

                        case "0":
                            return;
                    }

                } while (true);
            }
        }

        private static void UpdateSize(ISizeService service)
        {
            Console.Clear();
            Console.WriteLine("UPDATE SIZE:");

            ShowSizes(service);

            Console.WriteLine("SELECT ID SIZE TO UPDATE:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }

            var sizeUpdate = service.GetForUpdate(id);
            if (sizeUpdate.IsFailure)
            {
                ShowErrors(sizeUpdate.Errors);
                return;
            }

            var size = sizeUpdate.Value!;
            Console.WriteLine("NEW SIZE NUMBER: ");
            if(decimal.TryParse(Console.ReadLine(), out decimal newSizeNumber))
            {
                size.SizeNumber = newSizeNumber;
            }
            else
            {
                Console.WriteLine("INVALID SIZE NUMBER!");
                Console.ReadLine();
                return;
            }

            var result = service.Update(size);
            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("SIZE UPDATED SUCCESSFULLY!");
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadLine();
        }

        private static void DeleteSize(ISizeService service)
        {
            Console.Clear();
            Console.WriteLine("DELETE SIZE:");
            ShowSizes(service);

            Console.WriteLine("SELECT ID SIZE TO DELETE:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("INVALID ID!");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("ARE YOU SURE TO DELETE SIZE? (Y/N)");
            var confirm = Console.ReadLine();
            if (confirm?.ToUpper() != "Y")
            {
                Console.WriteLine("DELETE CANCELLED!");
                Console.ReadLine();
                return;
            }

            var sizeResult = service.Delete(id);

            if (sizeResult.IsFailure)
            {
                ShowErrors(sizeResult.Errors);
            }
            else
            {
                Console.WriteLine("SIZE DELETED SUCCESSFULLY!");
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadLine();
        }

        private static void AddSize(ISizeService service)
        {
            Console.Clear();
            Console.WriteLine("ADD SIZE:");

            var dto = new SizeCreateDto();

            Console.WriteLine("SIZE NUMBER: ");
            dto.SizeNumber = decimal.TryParse(Console.ReadLine(), out decimal sizeNumber) ? sizeNumber : 0;//aca lo que estoy haciendo es intentar parsear el input del usuario a decimal, si no se puede parsear, asigno 0 por defecto

            var result = service.Add(dto);

            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("SIZE ADDED SUCCESSFULLY!");
            }
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadKey();
        }

        private static void ListSizes(ISizeService service)
        {
            Console.Clear();
            Console.WriteLine("LIST OF SIZES:");
            ShowSizes(service);
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadLine();
        }

        private static void ShowSizes(ISizeService service)
        {
            
            var sizeResult = service.GetAll();
            if (sizeResult.IsFailure)
            {
                ShowErrors(sizeResult.Errors);
                return;
            }

            var sizes = sizeResult.Value;
            foreach (var size in sizes!)
            {
                Console.WriteLine($"ID: {size.SizeId} -- Number: {size.SizeNumber}");
            }
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
