using GamerSellStore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Persistence.Configurations
{
    public class TituloConfiguration : IEntityTypeConfiguration<Titulo>
    {
        public void Configure(EntityTypeBuilder<Titulo> builder)
        {
            builder.Property(p => p.Nombre)
                .HasMaxLength(200);

            builder.Property(p => p.Descripcion)
                .HasMaxLength(200);

            builder.Property(p => p.ImageUrl)
                .IsUnicode(false)
                .HasMaxLength(500);

            builder.HasIndex(p => p.Nombre);
            builder.ToTable("Titulo", schema: "GamerShop");
        }
    }
}
