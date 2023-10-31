using GamerSellStore.Entities.Info;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Persistence
{
    //public class GamerSellStoreDbContext : DbContext
    public class GamerSellStoreDbContext : IdentityDbContext<GamerSellStoreUserIdentity>
    {
        public GamerSellStoreDbContext(DbContextOptions<GamerSellStoreDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ReservaInfo>().HasNoKey(); //Se especifica no es una tabla, por lo tanto no tiene llave primaria
            modelBuilder.Entity<EvaluacionInfo>().HasNoKey();
            //Fluent API
            //modelBuilder.Entity<Genero>()
            //    .Property(p => p.Nombre)
            //    .HasMaxLength(100);
            //Para usar esquemas
            //modelBuilder.Entity<Genero>()
            //   .ToTable("Generos", schema: "Gamers");

            modelBuilder.HasDefaultSchema("GamerShop");

            //Cambiandole de nombre a las tablas de user identity 
            modelBuilder.Entity<GamerSellStoreUserIdentity>(e => e.ToTable("Usuario"));
            modelBuilder.Entity<IdentityRole>(e => e.ToTable("Rol"));
            modelBuilder.Entity<IdentityUserRole<string>>(e => e.ToTable("UsuarioRol"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-BR1N61U\\SQLEXPRESS;Database=BD_GAMER;User Id=sa;Password=*kunfu123;Encrypt=False;",
                    b => b.MigrationsAssembly("20231022232451_Migracion001"));
            }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            // Con esto eliminamos el Cascade como metodo principal para la eliminacion de registros
            configurationBuilder.Conventions.Remove(typeof(CascadeDeleteConvention));
        }
    }
}
