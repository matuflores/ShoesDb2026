using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.DTOs.Shoe
{
    public class ShoeDetailsDto
    {
        public int ShoeId { get; set; }
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        //public bool Active { get; set; } = true;
        public string BrandName { get; set; } = null!;
        public string SportName { get; set; } = null!;
        public string GenreName { get; set; } = null!;
        public decimal SizeNumber { get; set; }
    }
}
