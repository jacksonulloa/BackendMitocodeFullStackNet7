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
    public class ConsolaConfiguration : IEntityTypeConfiguration<Consola>
    {
        public void Configure(EntityTypeBuilder<Consola> builder)
        {
            builder.Property(p => p.Nombre)
                .HasMaxLength(200);
            builder.ToTable("Consola", schema: "GamerShop");
            builder
                .HasData(
                    new Consola { Id = 1, Nombre = "ATARI", Estado = true },
                    new Consola { Id = 2, Nombre = "NES", Estado = true },
                    new Consola { Id = 3, Nombre = "MAME", Estado = true },
                    new Consola { Id = 4, Nombre = "GENESIS", Estado = true },
                    new Consola { Id = 5, Nombre = "SNES", Estado = true },
                    new Consola { Id = 6, Nombre = "SEGA SATURN", Estado = true },
                    new Consola { Id = 7, Nombre = "PS1", Estado = true },
                    new Consola { Id = 8, Nombre = "PS2", Estado = true },
                    new Consola { Id = 9, Nombre = "PS3", Estado = true },
                    new Consola { Id = 10, Nombre = "PS4", Estado = true },
                    new Consola { Id = 11, Nombre = "PS5", Estado = true },
                    new Consola { Id = 12, Nombre = "PSP", Estado = true },
                    new Consola { Id = 13, Nombre = "PSVita", Estado = true },
                    new Consola { Id = 14, Nombre = "Xbox one", Estado = true },
                    new Consola { Id = 15, Nombre = "Xbox 360", Estado = true },
                    new Consola { Id = 16, Nombre = "Xbox Series X", Estado = true },
                    new Consola { Id = 17, Nombre = "PC", Estado = true },
                    new Consola { Id = 18, Nombre = "Arcade", Estado = true },
                    new Consola { Id = 19, Nombre = "GBA", Estado = true },
                    new Consola { Id = 20, Nombre = "GBA Advanced", Estado = true },
                    new Consola { Id = 21, Nombre = "Nintendo Switch", Estado = true },
                    new Consola { Id = 22, Nombre = "WII", Estado = true },
                    new Consola { Id = 23, Nombre = "WIIU", Estado = true }
                );
        }
    }
}
