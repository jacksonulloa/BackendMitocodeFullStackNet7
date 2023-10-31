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
    public class GeneroConfiguration : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder
                .Property(p => p.Nombre)
                .HasMaxLength(100);
            builder.ToTable("Genero", schema: "GamerShop");
            builder
                .HasData(
                    new Genero { Id = 1, Nombre = "Survival Horror", Estado = true },
                    new Genero { Id = 2, Nombre = "RPG", Estado = true },
                    new Genero { Id = 3, Nombre = "SCI-FI", Estado = true },
                    new Genero { Id = 4, Nombre = "Shooter", Estado = true },
                    new Genero { Id = 5, Nombre = "Aventura", Estado = true },
                    new Genero { Id = 6, Nombre = "Fantasia", Estado = true },
                    new Genero { Id = 7, Nombre = "Deporte", Estado = true },
                    new Genero { Id = 8, Nombre = "Musical", Estado = true },
                    new Genero { Id = 9, Nombre = "Automoviles", Estado = true },
                    new Genero { Id = 10, Nombre = "Medieval", Estado = true }
                );
            //builder.HasQueryFilter(x => x.Estado); Sirve para traer objetos que cumplen solo con esa condicion evitando filtros adicionales en lista
        }
    }
}
