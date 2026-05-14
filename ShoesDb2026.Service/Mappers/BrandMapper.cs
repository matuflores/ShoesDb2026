using ShoesDb2026.Entities;
using ShoesDb2026.Service.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Mappers
{
    public static class BrandMapper
    {
        public static Brand ToEntity(BrandCreateDto dto)
        {
            return new Brand
            {
                BrandName = dto.BrandName,
                ImageUrl = dto.ImageUrl
            };
        }
        public static BrandListDto ToListDto(Brand brand)
        {
            return new BrandListDto
            {
                BrandId = brand.BrandId,
                BrandName = brand.BrandName
            };
        }

        public static BrandUpdateDto ToUpdateDto(Brand brand) 
        {
            return new BrandUpdateDto
            {
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                ImageUrl = brand.ImageUrl
            };
        }
    }
}
