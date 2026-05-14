using ShoesDb2026.Entities;
using ShoesDb2026.Service.DTOs.Brand;
using ShoesDb2026.Service.DTOs.Size;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Mappers
{
    public static class SizeMapper
    {
        public static Size ToEntity(SizeCreateDto dto)
        {
            return new Size
            {
                SizeNumber = dto.SizeNumber
            };
        }
        public static SizeListDto ToListDto(Size size)
        {
            return new SizeListDto
            {
                SizeId = size.SizeId,
                SizeNumber = size.SizeNumber
            };
        }
        public static SizeUpdateDto ToUpdateDto(Size size)
        {
            return new SizeUpdateDto
            {
                SizeId = size.SizeId,
                SizeNumber = size.SizeNumber
            };
        }
    }
}
