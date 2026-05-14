using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Interfaces
{
    public interface ISizesRepository
    {
        List<Size> GetAll();
        IQueryable<Size> Query();
        Size? GetById(int id);
        void Delete(int id);
        void Update(Size size);
        void Add(Size size);
        bool ExistSameName(decimal sizeNumber, int? sizeId = null);

        bool HasShoes(int sizeId);
    }
}
