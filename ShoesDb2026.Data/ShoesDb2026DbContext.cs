using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data
{
    public class ShoesDb2026DbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ShoesDb2026; Trusted_Connection=true; TrustServerCertificate=true;");
        }
    }
}
