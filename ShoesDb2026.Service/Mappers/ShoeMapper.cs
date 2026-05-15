using ShoesDb2026.Entities;
using ShoesDb2026.Service.DTOs.Shoe;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Mappers
{
    public static class ShoeMapper
    {
        public static Shoe ToEntity(ShoeCreateDto dto)
        {
            return new Shoe
            {
                Model = dto.Model,
                Description = dto.Description,
                Price = dto.Price,
                BrandId = dto.BrandId,
                SportId = dto.SportId,
                GenreId = dto.GenreId,
                SizeId = dto.SizeId
            };
        }

        public static ShoeDetailsDto ToDetailsDto(Shoe shoe)
        {
            return new ShoeDetailsDto
            {
                ShoeId = shoe.ShoeId,
                Model = shoe.Model,
                Description = shoe.Description,
                Price = shoe.Price,
                BrandName = shoe.Brand.BrandName,
                SportName = shoe.Sport.SportName,
                GenreName = shoe.Genre.GenreName,
                SizeNumber = shoe.Size.SizeNumber
            };
        }

        public static ShoeListDto ToListDto(Shoe shoe)
        {
            return new ShoeListDto
            {
                ShoeId = shoe.ShoeId,
                Model = shoe.Model,
                Price = shoe.Price,
                BrandName = shoe.Brand.BrandName,
                SportName = shoe.Sport.SportName,
                GenreName = shoe.Genre.GenreName,
                SizeNumber = shoe.Size.SizeNumber
            };
        }

        public static ShoeUpdateDto ToUpdateDto(Shoe shoe)
        {
            return new ShoeUpdateDto
            {
                ShoeId = shoe.ShoeId,
                Model = shoe.Model,
                Description = shoe.Description,
                Price = shoe.Price,
                BrandId = shoe.BrandId,
                SportId = shoe.SportId,
                GenreId = shoe.GenreId,
                SizeId = shoe.SizeId
            };
        }
    }
}
