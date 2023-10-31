using Azure;
using GamerSellStore.Dtos.Request.Usuario;
using GamerSellStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace GamerSellStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> ilogger;

        public UserController(IUserService _userService, ILogger<UserController> _ilogger)
        {
            userService = _userService;
            ilogger = _ilogger;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDtoRequest request)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(request)}]");
            var response = await userService.LoginAsync(request);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : Unauthorized(response);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUsuarioAsync([FromBody] RegisterDtoRequest request)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(request)}]");
            var response = await userService.RegistrarUsuarioAsync(request);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : Unauthorized(response);
        }

        [HttpPost]
        public async Task<IActionResult> RequestTokenToResetPassword(ResetPasswordDtoRequest request)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(request)}]");
            var response = await userService.RequestTokenToResetPasswordAsync(request);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ConfirmPasswordDtoRequest request)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(request)}]");
            var response = await userService.ResetPasswordAsync(request);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDtoRequest request)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(request)}]");
            // Aqui recupero el correo del usuario autenticado.
            var email = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Email).Value;
            var response = await userService.ChangePasswordAsync(email, request);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }
    }
}
