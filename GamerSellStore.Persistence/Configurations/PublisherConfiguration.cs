using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSellStore.Entities;

namespace GamerSellStore.Persistence.Configurations
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder
                .Property(p => p.Nombre)
                .HasMaxLength(100);
            builder
                .Property(p => p.Pais)
                .HasMaxLength(35);
            builder.ToTable("Publisher", schema: "GamerShop");
            builder
                .HasData(
                    new Publisher { Id = 1, Nombre = "Microsoft Game Studios", Pais = "USA", Estado = true },
                    new Publisher { Id = 2, Nombre = "RockStar Games", Pais = "USA", Estado = true },
                    new Publisher { Id = 3, Nombre = "EA TM", Pais = "USA", Estado = true },
                    new Publisher { Id = 4, Nombre = "SEGA", Pais = "USA", Estado = true },
                    new Publisher { Id = 5, Nombre = "CAPCOM", Pais = "JPN", Estado = true },
                    new Publisher { Id = 6, Nombre = "UBISOFT", Pais = "USA", Estado = true },
                    new Publisher { Id = 7, Nombre = "Insomniac Games", Pais = "USA", Estado = true },
                    new Publisher { Id = 8, Nombre = "Warner Bros Games", Pais = "USA", Estado = true },
                    new Publisher { Id = 9, Nombre = "Sony Interactive Games", Pais = "JPN", Estado = true },
                    new Publisher { Id = 10, Nombre = "THQ", Pais = "AUT", Estado = true },
                    new Publisher { Id = 11, Nombre = "Blizzard Entertaiment", Pais = "USA", Estado = true },
                    new Publisher { Id = 12, Nombre = "Telltale Games", Pais = "USA", Estado = true },
                    new Publisher { Id = 13, Nombre = "SquareEnix", Pais = "JPN", Estado = true },
                    new Publisher { Id = 14, Nombre = "Nintendo", Pais = "JPN", Estado = true },
                    new Publisher { Id = 15, Nombre = "Machine Zone", Pais = "USA", Estado = true },
                    new Publisher { Id = 16, Nombre = "EIDOS Interactive Ltd.", Pais = "UK", Estado = true },
                    new Publisher { Id = 17, Nombre = "Bethesda Softwork", Pais = "CHN", Estado = true },
                    new Publisher { Id = 18, Nombre = "Konami", Pais = "JPN", Estado = true },
                    new Publisher { Id = 19, Nombre = "Santa Monica Studio", Pais = "USA", Estado = true }
                );
        }
    }
}
