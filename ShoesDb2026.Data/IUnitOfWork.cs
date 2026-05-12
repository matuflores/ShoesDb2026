using ShoesDb2026.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data
{
    public interface IUnitOfWork
    {
        IBrandsRepository Brands { get; }
        IGenresRepository Genres { get; }
        IShoesRepository Shoes { get; }
        ISizesRepository Sizes { get; }
        ISportsRepository Sports { get; }
        void Save();
    }
}
