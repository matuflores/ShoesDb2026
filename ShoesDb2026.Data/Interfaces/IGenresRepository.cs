using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Interfaces
{
    public interface IGenresRepository
    {
        //List<Genre> GetAll();
        //IQueryable<Genre> Query();
        //Genre? GetById(int id);
        //void Delete(int id);
        //void Update(Genre genre);
        //void Add(Genre genre);
        //bool ExistSameName(string genreName,int? genreId=null);
        //bool HasShoes(int genreId);

        IEnumerable<Genre> GetAll(); //CON ESTO LO QUE HAGO ES DEVOLVER UNA COLECCION DE GENEROS, PERO NO DEVUELVE UN QUERYABLE, SINO UNA COLECCION DE GENEROS, POR LO QUE NO SE PUEDE HACER UN .WHERE, UN .SELECT, ETC. EN EL CONTROLADOR, PORQUE NO DEVUELVE UN QUERYABLE, SINO UNA COLECCION DE GENEROS.
    }
}
