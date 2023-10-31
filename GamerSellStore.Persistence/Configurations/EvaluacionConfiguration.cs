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
    public class EvaluacionConfiguration : IEntityTypeConfiguration<Evaluacion>
    {
        public void Configure(EntityTypeBuilder<Evaluacion> builder)
        {
            builder.Property(p => p.Resenia)
                .HasMaxLength(500);

            builder.Property(p => p.Fecha)
                //.HasColumnType("date")
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.ToTable("Evaluacion", schema: "GamerShop");
        }
    }
}
