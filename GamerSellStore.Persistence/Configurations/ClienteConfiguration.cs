using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSellStore.Entities;

namespace GamerSellStore.Persistence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.Property(p => p.Correo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(p => p.Nombre)
                .HasMaxLength(200);

            builder.Property(p => p.Sexo)
                .HasMaxLength(1);

            builder.Property(p => p.Usuario)
                .HasMaxLength(50);

            builder.Property(p => p.Pais)
                .HasMaxLength(50);

            builder.ToTable("Cliente", schema: "GamerShop");
            builder
                .HasData(
                    new Cliente { Id = 1, Nombre = "Juan Perez", Correo="jperez@gmail.com", Pais="ECU", Sexo="M", Usuario="jperez", Estado = true },
                    new Cliente { Id = 2, Nombre = "Martin Palermo", Correo = "mpalermo@gmail.com", Pais = "PER", Sexo = "M", Usuario = "mpalermo", Estado = true },
                    new Cliente { Id = 3, Nombre = "Eduardo Martinez", Correo = "emartinez@gmail.com", Pais = "PER", Sexo = "M", Usuario = "emartinez", Estado = true },
                    new Cliente { Id = 4, Nombre = "Lorena Quevedo", Correo = "lquevedo@gmail.com", Pais = "BOL", Sexo = "F", Usuario = "lquevedo", Estado = true },
                    new Cliente { Id = 5, Nombre = "Juan Iriarte", Correo = "jiriarte@gmail.com", Pais = "ARG", Sexo = "M", Usuario = "jiriarte", Estado = true },
                    new Cliente { Id = 6, Nombre = "Manuela Solano", Correo = "msolano@gmail.com", Pais = "BRA", Sexo = "F", Usuario = "msolano", Estado = true },
                    new Cliente { Id = 7, Nombre = "Iris Pedraza", Correo = "ipedraza@gmail.com", Pais = "PER", Sexo = "F", Usuario = "ipedraza", Estado = true },
                    new Cliente { Id = 8, Nombre = "Luz Bernaechea", Correo = "lbernaechea@gmail.com", Pais = "ARG", Sexo = "F", Usuario = "lbernaechea", Estado = true }
                );
        }
    }
}
