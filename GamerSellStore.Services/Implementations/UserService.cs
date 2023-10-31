using GamerSellStore.Dtos.Request.Correo;
using GamerSellStore.Dtos.Request.Usuario;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Dtos.ResponseBase;
using GamerSellStore.Entities;
using GamerSellStore.Persistence;
using GamerSellStore.Repositories;
using GamerSellStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GamerSellStore.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<GamerSellStoreUserIdentity> userManager;
        private readonly ILogger<UserService> ilogger;
        private readonly IOptions<AppSettings> options;
        private readonly IClienteRepository clienteRepository;
        private readonly ICorreo icorreo;
        public UserService(UserManager<GamerSellStoreUserIdentity> _userManager, ILogger<UserService> _ilogger,
            IOptions<AppSettings> _options, IClienteRepository _clienteRepository, ICorreo _icorreo)
        {
            userManager = _userManager;
            ilogger = _ilogger;
            options = _options;
            clienteRepository = _clienteRepository;
            icorreo = _icorreo;
        }

        public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
        {
            LoginDtoResponse response = new LoginDtoResponse();
            try
            {
                var identity = await userManager.FindByEmailAsync(request.Username);
                if (identity is null)
                {
                    response.codResp = "16";
                    response.descResp = $"El usuario no se encuentra registrado";
                    response.NombreCompleto = "";
                    response.Token = "";
                    response.Roles = new List<string>();
                    return response;
                }
                if (await userManager.IsLockedOutAsync(identity))
                {
                    response.codResp = "17";
                    response.descResp = $"El usuario se encuentra bloqueado, contacte a soporte";
                    response.NombreCompleto = "";
                    response.Token = "";
                    response.Roles = new List<string>();
                    return response;
                }
                var result = await userManager.CheckPasswordAsync(identity, request.Password);
                if (!result)
                {
                    await userManager.AccessFailedAsync(identity);
                    int intentos_restantes = 5 - identity.AccessFailedCount;
                    response.codResp = "18";
                    response.descResp = $"El password es incorrecto, quedan {intentos_restantes} para que se bloquee la cuenta";
                    response.NombreCompleto = "";
                    response.Token = "";
                    response.Roles = new List<string>();

                    ilogger.LogWarning($"Error de autenticacion para el usuario {identity.UserName}");
                    return response;
                }
                var roles = await userManager.GetRolesAsync(identity);

                //Seteando la fecha de expiracion en un día a partir de este momento
                var fechaExpiracion = DateTime.Now.AddDays(1);

                //Declarando los claims para el payload
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, $"{identity.FirstName} {identity.LastName}"),
                    new Claim(ClaimTypes.Email, request.Username),
                    new Claim(ClaimTypes.Expiration, fechaExpiracion.ToString("yyyy-MM-dd HH:mm:ss"))
                };

                foreach (var _role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, _role));
                }

                response.Roles = new List<string>();
                response.Roles.AddRange(roles);

                //Creacion del JWT
                var symetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.jwt.SecretKey));

                var credentials = new SigningCredentials(symetricKey, SecurityAlgorithms.HmacSha256);
                var header = new JwtHeader(credentials);
                var payload = new JwtPayload(issuer: options.Value.jwt.Issuer,
                    audience: options.Value.jwt.Audience,
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: fechaExpiracion);
                var token = new JwtSecurityToken(header, payload);

                response.codResp = "00";
                response.descResp = "Conforme";
                response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                response.NombreCompleto = $"{identity.FirstName} {identity.LastName}";
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.NombreCompleto = "";
                response.Token = "";
                response.Roles = new List<string>();
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponse> RegistrarUsuarioAsync(RegisterDtoRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var user = new GamerSellStoreUserIdentity()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    UserName = request.Email,
                    Age = request.Age,
                    DocumentNumber = request.DocumentNumber,
                    DocumentType = (DocumentTypeEnum)request.DocumentType,
                    EmailConfirmed = true
                };
                var cliente = await clienteRepository.BuscarPorEmailAsync(user.Email);
                if (cliente is not null)
                {
                    response = new BaseResponse();
                    response.codResp = "99";
                    response.descResp = $"El cliente con correo {user.Email} ya se encuentra registrado";
                }
                else
                {
                    var result = await userManager.CreateAsync(user, request.ConfirmPassword);
                    if (result.Succeeded)
                    {
                        user = await userManager.FindByEmailAsync(request.Email);
                        if (user is not null)
                        {
                            //Considerando para el ejemplo que fuera del administrador nadia mas puede tener ese rol
                            await userManager.AddToRoleAsync(user, Constantes.RolCustomer);
                            cliente = new Cliente()
                            {
                                Correo = user.Email,
                                Nombre = $"{user.FirstName} {user.LastName}",
                                Estado = true,
                                Pais = "Peru", //NO DEFINIDO por tiempo de implementacion no llegue a poner este parametro
                                Sexo = "I", //NO DEFINIDO por tiempo de implementacion no llegue a poner este parametro
                                Usuario = user.Email
                            };

                            await clienteRepository.AgregarAsync(cliente);
                            //Enviar correo electronico (Utilizo gmx.es debido a que la opcion mostrada en clase no me aparece en mi cuenta de outlook)
                            CorreoDtoRequest requestcorreo = new()
                            {
                                Destinatario = cliente.Correo,
                                Asunto = $"Creacion de cuenta {cliente.Correo}",
                                Mensaje = $"Se ha generado satisfactoriamente su cuenta"
                            };
                            await icorreo.EnviarCorreoAsync(requestcorreo);

                            response.codResp = "00";
                            response.descResp = $"Se registro el usuario {user.UserName} con Id {user.Id}";
                        }
                    }
                    else
                    {
                        response.codResp = "99";
                        var sb = new StringBuilder();
                        foreach (var error in result.Errors)
                        {
                            sb.AppendLine(error.Description);
                        }
                        response.descResp = sb.ToString();
                        sb.Clear(); // liberacion de memoria
                    }
                }
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponse> RequestTokenToResetPasswordAsync(ResetPasswordDtoRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userIdentity = await userManager.FindByEmailAsync(request.Email);
                if (userIdentity is null)
                {
                    response.codResp = "99";
                    response.descResp = $"El usuario no se encuentra registrado";
                }
                else
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(userIdentity);
                    // Enviar un email con el token para reestablecer la contraseña
                    CorreoDtoRequest email = new()
                    {
                        Asunto = $"Restablecimiento de cuenta de usuario {userIdentity.UserName}",
                        Destinatario = request.Email,
                        Mensaje = @$"<p> Estimado {userIdentity.FirstName} {userIdentity.LastName}</p>
                                    <p>Para reestablecer su clave, por favor copie el siguiente codigo</p>
                                    <p><strong> {token} </strong></p>
                                    <hr/>
                                    Atte. <br/>
                                    GamerSellStore © 2023"
                    };
                    await icorreo.EnviarCorreoAsync(email);
                    response.codResp = "00";
                    response.descResp = "Token de restablecimiento enviado";
                }
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponse> ResetPasswordAsync(ConfirmPasswordDtoRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userIdentity = await userManager.FindByEmailAsync(request.Email);
                if (userIdentity is null)
                {
                    response.codResp = "99";
                    response.descResp = $"El usuario no se encuentra registrado";
                }
                else
                {
                    var result = await userManager.ResetPasswordAsync(userIdentity, request.Token, request.ConfirmPassword);
                    if (!result.Succeeded)
                    {
                        var sb = new StringBuilder();
                        foreach (var error in result.Errors)
                        {
                            sb.AppendLine(error.Description);
                        }
                        response.codResp = "99";
                        response.descResp = sb.ToString();
                        sb.Clear(); // limpiamos la memoria 
                    }
                    else
                    {
                        CorreoDtoRequest email = new()
                        {
                            Asunto = $"Confirmacion de cambio de clave de usuario {userIdentity.UserName}",
                            Destinatario = request.Email,
                            Mensaje = @$"<p> Estimado {userIdentity.FirstName} {userIdentity.LastName}</p>
                                    <p>Se ha cambiado su clave correctamente</p>
                                    <hr/>
                                    Atte. <br/>
                                    GamerSellStore © 2023"
                        };
                        // Enviar un email de confirmacion de clave cambiada
                        await icorreo.EnviarCorreoAsync(email);
                        response.codResp = "00";
                        response.descResp = "Correo de confirmacion  de clave enviado";
                    }
                }
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponse> ChangePasswordAsync(string email, ChangePasswordDtoRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var userIdentity = await userManager.FindByEmailAsync(email);
                if (userIdentity is null)
                {
                    response.codResp = "99";
                    response.descResp = $"El usuario no se encuentra registrado";
                }
                else
                {
                    var result = await userManager.ChangePasswordAsync(userIdentity, 
                        request.OldPassword, request.NewPassword);
                    if (!result.Succeeded)
                    {
                        var sb = new StringBuilder();
                        foreach (var error in result.Errors)
                        {
                            sb.AppendLine(error.Description);
                        }
                        response.codResp = "99";
                        response.descResp = sb.ToString();
                        sb.Clear(); // limpiamos la memoria 
                    }
                    else
                    {
                        ilogger.LogInformation("Se cambio la clave para {email}", userIdentity.Email);

                        // Enviar un email de confirmacion de clave cambiada
                        CorreoDtoRequest objEmail = new()
                        {
                            Asunto = $"Confirmacion de cambio de clave de usuario {userIdentity.UserName}",
                            Destinatario = email,
                            Mensaje = @$"<p> Estimado {userIdentity.FirstName} {userIdentity.LastName}</p>
                                    <p>Se ha cambiado su clave correctamente</p>
                                    <hr/>
                                    Atte. <br/>
                                    GamerSellStore © 2023"
                        };
                        await icorreo.EnviarCorreoAsync(objEmail);
                        response.codResp = "00";
                        response.descResp = "Se envio correo de confirmacion satisfactoriamente";
                    }
                }
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }
    }
}
