using Microsoft.EntityFrameworkCore;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data
{
    public class ShoesDb2026DbContext:DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Sport> Sports { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ShoesDb2026; Trusted_Connection=true; TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shoe>(entity =>
            {
                entity.HasOne(s => s.Size)
                      .WithMany()
                      .HasForeignKey(s => s.SizeId);
            });//aca lo que esta haciendo es configurar la relacion entre SportShoe y SiZe, le estoy diciendo que SportShoe tiene una propiedad de navegacion llamada Size, que es una relacion de uno a muchos, es decir, un SportShoe tiene un SiZe, pero un SiZe puede tener muchos SportShoes, y le estoy diciendo que la clave foranea de esta relacion es SizeId, que se encuentra en la tabla de SportShoe. Esto es necesario porque por defecto Entity Framework Core no sabe como configurar esta relacion, ya que no hay una propiedad de navegacion en la clase SiZe que apunte a SportShoe, por lo tanto, tengo que configurarla manualmente en el metodo OnModelCreating.
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShoesDb2026DbContext).Assembly);

            foreach (var fk in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            
        }
    }
}
