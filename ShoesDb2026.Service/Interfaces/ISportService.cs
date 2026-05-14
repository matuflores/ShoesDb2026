using ShoesDb2026.Service.Common;
using ShoesDb2026.Service.DTOs.Brand;
using ShoesDb2026.Service.DTOs.Sport;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Interfaces
{
    public interface ISportService
    {
        Result<List<SportListDto>> GetAll();
        Result<SportUpdateDto> GetForUpdate(int id);

        Result Add(SportCreateDto sportDto);
        Result Update(SportUpdateDto sportDto);
        Result Delete(int id);
    }
}
