using GamerSellStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Persistence.Configurations
{
    public class ClasificacionConfiguration : IEntityTypeConfiguration<Clasificacion>
    {
        public void Configure(EntityTypeBuilder<Clasificacion> builder)
        {
            builder.Property(p => p.Nombre)
                .HasMaxLength(5);

            builder.ToTable("Clasificacion", schema: "GamerShop");
            builder
                .HasData(
                    new Clasificacion { Id = 1, Nombre = "E", Estado = true },
                    new Clasificacion { Id = 2, Nombre = "E10+", Estado = true },
                    new Clasificacion { Id = 3, Nombre = "T", Estado = true },
                    new Clasificacion { Id = 4, Nombre = "M", Estado = true },
                    new Clasificacion { Id = 5, Nombre = "AO", Estado = true },
                    new Clasificacion { Id = 6, Nombre = "RP", Estado = true }
                );
        }
    }
}
