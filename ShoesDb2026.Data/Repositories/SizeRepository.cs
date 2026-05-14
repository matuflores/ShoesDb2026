using Microsoft.EntityFrameworkCore;
using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Repositories
{
    public class SizeRepository : ISizesRepository
    {
        private readonly ShoesDb2026DbContext _context;

        public SizeRepository(ShoesDb2026DbContext context)
        {
            _context = context;
        }
        public void Add(Size size)
        {
            _context.Sizes.Add(size);
        }

        public void Delete(int id)
        {
            var size = _context.Sizes.Find(id);
            if (size == null) return;
            _context.Sizes.Remove(size);
        }

        public bool ExistSameName(decimal sizeNumber, int? sizeId = null)
        {
            //return _context.Sizes.Any(s => s.SizeNumber == sizeNumber && s.SizeId != sizeId);
            return _context.Sizes.Any(s => s.SizeNumber == sizeNumber && (sizeId ==null || s.SizeId!= sizeId));
        }

        public List<Size> GetAll()
        {
            return _context.Sizes.AsNoTracking().ToList();
        }

        public Size? GetById(int id)
        {
            return _context.Sizes.Find(id);
        }

        public bool HasShoes(int sizeId)
        {
            return _context.Shoes.Any(s => s.SizeId == sizeId);
        }

        public IQueryable<Size> Query()
        {
            return _context.Sizes.AsNoTracking().AsQueryable();
        }

        public void Update(Size size)
        {
            _context.Sizes.Update(size);
        }
    }
}
