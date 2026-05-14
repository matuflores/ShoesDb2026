using Microsoft.EntityFrameworkCore;
using ShoesDb2026.Data.Interfaces;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Repositories
{
    public class GenreRepository : IGenresRepository
    {
        private readonly ShoesDb2026DbContext _context;

        public GenreRepository(ShoesDb2026DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetAll()
        {
            return _context.Genres.AsNoTracking().ToList();
        }
        //public void Add(Genre genre)
        //{
        //    _context.Genres.Add(genre);
        //}

        //public void Delete(int id)
        //{
        //    var genre = _context.Genres.Find(id);
        //    if (genre == null) return;
        //    _context.Genres.Remove(genre);
        //}

        //public bool ExistSameName(string genreName, int? genreId = null)
        //{
        //    return _context.Genres.Any(g => g.GenreName == genreName && g.GenreId != genreId);
        //}

        //public List<Genre> GetAll()
        //{
        //    return _context.Genres.AsNoTracking().ToList();
        //}

        //public Genre? GetById(int id)
        //{
        //    return _context.Genres.Find(id);
        //}

        //public bool HasShoes(int genreId)
        //{
        //    return _context.Shoes.Any(s => s.GenreId == genreId);
        //}

        //public IQueryable<Genre> Query()
        //{
        //    return _context.Genres.AsNoTracking().AsQueryable();
        //}

        //public void Update(Genre genre)
        //{
        //    _context.Genres.Update(genre);
        //}
    }
}
