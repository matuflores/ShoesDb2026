using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Repositories
{
    public class ShoeRepository : IShoesRepository
    {
        private readonly ShoesDb2026DbContext _context;
        public ShoeRepository(ShoesDb2026DbContext context)
        {
            _context = context;
        }
        public void Add(Shoe shoe)
        {
            _context.Shoes.Add(shoe);
        }

        public void Delete(int id)
        {
            var shoe = _context.Shoes.Find(id);
            if(shoe == null) return;
            _context.Shoes.Remove(shoe);
        }

        public bool ExistSameName(string model, int brandId, int sizeId, int? shoeId = null)
        {
            return _context.Shoes.Any(s => s.Model == model && 
            s.BrandId == brandId && 
            s.SizeId == sizeId && 
            (shoeId==null || s.ShoeId != shoeId));
        }

        public List<Shoe> GetAll()
        {
            return _context.Shoes
                .Include(s => s.Brand)
                .Include(s => s.Sport)
                .Include(s => s.Genre)
                .Include(s => s.Size)
                .Where(s => s.Active)
                .AsNoTracking().ToList();
        }

        public List<Shoe> GetByBrand(int brandId)
        {
            return _context.Shoes.Include(s => s.Brand)
                    .Include(s => s.Size)
                    .Include(s => s.Sport)
                    .Include(s => s.Genre)
                    .Where(s => s.BrandId == brandId && s.Active)
                    .AsNoTracking()
                    .ToList();
        }

        public Shoe? GetById(int id)
        {
            return _context.Shoes
                .Include(s => s.Brand)
                .Include(s => s.Sport)
                .Include(s => s.Genre)
                .FirstOrDefault(s => s.ShoeId == id);
        }

        public List<Shoe> GetBySize(int sizeId)
        {
            return _context.Shoes.Include(s => s.Brand)
                .Include(s => s.Size)
                .Include(s => s.Sport)
                .Include(s => s.Genre)
                .Where(s => s.SizeId == sizeId && s.Active)
                .AsNoTracking()
                .ToList();
        }

        public List<Shoe> GetBySport(int sportId)
        {
            return _context.Shoes.Include(s => s.Brand)
                .Include(s => s.Size)
                .Include(s => s.Sport)
                .Include(s => s.Genre)
                .Where(s => s.SportId == sportId && s.Active)
                .AsNoTracking()
                .ToList();
        }

        public List<Shoe> OrderByBrand()
        {
            return _context.Shoes.Include(s => s.Brand)
                .Include(s => s.Size)
                .Include(s => s.Sport)
                .Include(s => s.Genre)
                .Where(s => s.Active)
                .OrderBy(s => s.Brand.BrandName)
                .AsNoTracking()
                .ToList();
        }

        public List<Shoe> OrderByModel()
        {
            return _context.Shoes.Include(s => s.Brand)
                .Include(s => s.Size)
                .Include(s => s.Sport)
                .Include(s => s.Genre)
                .Where(s => s.Active)
                .OrderBy(s => s.Model)
                .AsNoTracking()
                .ToList();
        }

        public List<Shoe> OrderByPrice()
        {
            return _context.Shoes.Include(s => s.Brand)
                    .Include(s => s.Size)
                    .Include(s => s.Sport)
                    .Include(s => s.Genre)
                    .Where(s => s.Active)
                    .OrderBy(s => s.Price)
                    .AsNoTracking()
                    .ToList(); 
        }

        public IQueryable<Shoe> Query()
        {
            return _context.Shoes
                .Include(s => s.Brand)
                .Include(s => s.Genre)
                .Include(s => s.Sport)
                .AsNoTracking()
                .AsQueryable();
        }

        public void Update(Shoe shoe)
        {
            _context.Shoes.Update(shoe);
        }
    }
}
