using ShoesDb2026.Service.Common;
using ShoesDb2026.Service.DTOs.Size;
using ShoesDb2026.Service.DTOs.Sport;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Interfaces
{
    public interface ISizeService
    {
        Result<List<SizeListDto>> GetAll();
        Result<SizeUpdateDto> GetForUpdate(int id);

        Result Add(SizeCreateDto sizeDto);
        Result Update(SizeUpdateDto sizeDto);
        Result Delete(int id);
    }
}
