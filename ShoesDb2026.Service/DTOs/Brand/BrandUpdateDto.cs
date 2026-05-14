using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.DTOs.Brand
{
    public class BrandUpdateDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
