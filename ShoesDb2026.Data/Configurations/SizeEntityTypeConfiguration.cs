using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Data.Configurations
{
    public class SizeEntityTypeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.Property(s => s.SizeNumber).HasColumnType("decimal(3,1)").IsRequired();
            builder.HasIndex(s => s.SizeNumber).IsUnique();
        }
    }
}
