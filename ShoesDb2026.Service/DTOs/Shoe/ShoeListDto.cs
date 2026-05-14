using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.DTOs.Shoe
{
    public class ShoeListDto
    {
        public int ShoeId { get; set; }
        public string Model { get; set; } = null!;
        public decimal Price { get; set; }
        public string BrandName { get; set; } = null!;
        public string SportName { get; set; } = null!;
        public string GenreName { get; set; } = null!;
        public decimal SizeNumber { get; set; }
    }
}
