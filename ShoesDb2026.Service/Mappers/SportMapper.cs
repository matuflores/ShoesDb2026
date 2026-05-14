using ShoesDb2026.Entities;
using ShoesDb2026.Service.DTOs.Sport;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Mappers
{
    public static class SportMapper
    {
        public static Sport ToEntity(SportCreateDto dto)
        {
            return new Sport
            {
                SportName = dto.SportName
            };
        }

        public static SportListDto ToListDto(Sport sport)
        {
            return new SportListDto
            {
                SportId = sport.SportId,
                SportName = sport.SportName
            };
        }

        public static SportUpdateDto ToUpdateDto(Sport sport)
        {
            return new SportUpdateDto
            {
                SportId = sport.SportId,
                SportName = sport.SportName
            };
        }
    }
}
