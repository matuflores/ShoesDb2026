using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Repositories
{
    public class ShoeRepository : IShoesRepository
    {
        public void Add(Shoe shoe)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExistSameName(string model, int brandId, int sportId, int? shoeId = null)
        {
            throw new NotImplementedException();
        }

        public List<Shoe> GetAll()
        {
            throw new NotImplementedException();
        }

        public Shoe? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Shoe> Query()
        {
            throw new NotImplementedException();
        }

        public void Update(Shoe shoe)
        {
            throw new NotImplementedException();
        }
    }
}
