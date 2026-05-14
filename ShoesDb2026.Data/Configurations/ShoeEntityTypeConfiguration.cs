using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Configurations
{
    public class ShoeEntityTypeConfiguration : IEntityTypeConfiguration<Shoe>
    {
        public void Configure(EntityTypeBuilder<Shoe> builder)
        {
            builder.Property(s => s.Model).IsRequired().HasMaxLength(150);
            builder.HasIndex(s => s.Model).IsUnique();
            builder.Property(s=>s.Price).HasColumnType("decimal(10,2)");
        }
    }
}
