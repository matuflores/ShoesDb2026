using ShoesDb2026.Entities;
using ShoesDb2026.Service.Common;
using ShoesDb2026.Service.DTOs.Shoe;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Interfaces
{
    public interface IShoeService
    {
        Result<List<ShoeListDto>> GetAll();

        Result<ShoeDetailsDto> GetDetails(int id);

        Result<ShoeUpdateDto> GetForUpdate(int id);

        Result Add(ShoeCreateDto dto);

        Result Update(ShoeUpdateDto dto);

        Result Delete(int id);
        Result<List<Genre>> GetGenres();

        Result<List<ShoeListDto>> GetByBrand(int brandId);

        Result<List<ShoeListDto>> GetBySport(int sportId);

        Result<List<ShoeListDto>> GetBySize(int sizeId);

        Result<List<ShoeListDto>> OrderByModel();

        Result<List<ShoeListDto>> OrderByPrice();

        Result<List<ShoeListDto>> OrderByBrand();
    }
}
