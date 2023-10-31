using GamerSellStore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Persistence
{
    public static class UserDataSeeder
    {
        public static async Task Seed(IServiceProvider service)
        {
            //Retorna repositorio de usuarios
            var userManager = service.GetRequiredService<UserManager<GamerSellStoreUserIdentity>>();
            //Retorna repositorio de roles
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            var adminRole = new IdentityRole(Constantes.RolAdmin);
            var customerRole = new IdentityRole(Constantes.RolCustomer);

            //Creacion de los roles en la BD en caso no se encuentren registrados
            if (!await roleManager.RoleExistsAsync(Constantes.RolAdmin))
                await roleManager.CreateAsync(adminRole);
            if (!await roleManager.RoleExistsAsync(Constantes.RolCustomer))
                await roleManager.CreateAsync(customerRole);

            //Creacion del usuario admin
            var adminUser = new GamerSellStoreUserIdentity()
            {
                FirstName = "Administrador",
                LastName = "del Sistema",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                PhoneNumber = "999 999 999",
                Age = 30,
                DocumentType = DocumentTypeEnum.Dni,
                DocumentNumber = "12345678",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "Admin1234*");
            if (result.Succeeded)
            {
                // Obtenemos el registro del usuario
                adminUser = await userManager.FindByEmailAsync(adminUser.Email);
                // Aqui agregamos el Rol de Administrador para el usuario Admin
                if (adminUser is not null)
                    await userManager.AddToRoleAsync(adminUser, Constantes.RolAdmin);
            }
        }
    }
}
