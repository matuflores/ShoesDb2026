using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.DTOs.Shoe
{
    public class ShoeUpdateDto
    {
        public int ShoeId { get; set; }
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }

        public int BrandId { get; set; }
        public int SportId { get; set; }
        public int GenreId { get; set; }
        public int SizeId { get; set; }
    }
}
