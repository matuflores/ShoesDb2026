using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Entities
{
    public class Shoe
    {
        public int ShoeId { get; set; }
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public bool Active { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
        public int SportId { get; set; }
        public Sport Sport { get; set; } = null!;
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
        //chequear
    }
}
