using Microsoft.EntityFrameworkCore;
using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Repositories
{
    public class BrandRepository : IBrandsRepository
    {
        private readonly ShoesDb2026DbContext _context;

        public BrandRepository(ShoesDb2026DbContext context)
        {
            _context = context;
        }
        public void Add(Brand brand)
        {
            _context.Brands.Add(brand);
        }

        public void Delete(int id)
        {
            var brand = _context.Brands.Find(id);
            if (brand == null) return;
            _context.Brands.Remove(brand);
        }

        public bool ExistSameName(string brandName, int? brandId = null)
        {
            return _context.Brands.Any(b => b.BrandName == brandName && b.BrandId != brandId);
        }

        public List<Brand> GetAll()
        {
            return _context.Brands.AsNoTracking().ToList();
        }

        public Brand? GetById(int id)
        {
            return _context.Brands.Find(id);
        }

        public bool HasShoes(int brandId)
        {
            return _context.Shoes.Any(s => s.BrandId == brandId);
        }

        public IQueryable<Brand> Query()
        {
            return _context.Brands.AsNoTracking()
                .AsQueryable();
        }

        public void Update(Brand brand)
        {
            _context.Brands.Update(brand);
        }
    }
}
