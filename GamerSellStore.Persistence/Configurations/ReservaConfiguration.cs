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
    public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.Property(p => p.NroTxn)
                .IsUnicode(false)
                .HasMaxLength(20);

            builder.Property(p => p.FechaTxn)
                //.HasColumnType("date")
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(p => p.NroTxn);
            builder.ToTable("Reserva", schema: "GamerShop");
        }
    }
}
