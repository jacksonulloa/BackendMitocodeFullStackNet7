using Azure.Core;
using GamerSellStore.Dtos.Request;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Entities;
using GamerSellStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace GamerSellStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TituloController : ControllerBase
    {
        private readonly ITituloService service;
        private readonly ILogger<TituloController> ilogger;

        public TituloController(ITituloService _service, ILogger<TituloController> _ilogger)
        {
            service = _service;
            ilogger = _ilogger;
        }

        [HttpGet("Listar")]
        [Authorize]
        public async Task<IActionResult> ListarAsync()
        {
            //VERIFICACION MANUAL DEL VENCIMIENTO DEL TOKEN
            var fechaExpiracion = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Expiration)!.Value;
            
            if (fechaExpiracion is not null && DateTime.Parse(fechaExpiracion) <= DateTime.Now)
            {
                return BadRequest(new LoginDtoResponse()
                {
                    codResp = "99",
                    descResp = $"El token ya ha expirado a las {fechaExpiracion}",
                    NombreCompleto = "",
                    Roles = new List<string>(),
                    Token = ""
                });
            }

            var response = await service.ListarAsync();
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ListarFiltrado")]
        [Authorize]
        public async Task<IActionResult> ListarFiltradoAsync([FromQuery] TituloSearchDtoRequest request)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(request)}]");
            var response = await service.ListarAsync(request);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ListarPaginado")]
        [Authorize]
        public async Task<IActionResult> ListarPaginadoAsync([FromQuery] TituloBusqueda request, CancellationToken cancellationToken = default)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(request)}]");
            var response = await service.ListarAsync(request, cancellationToken);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("BuscarPorId")]
        [Authorize]
        public async Task<IActionResult> BuscarPorIdAsync(int id)
        {
            ilogger.LogInformation($"id:[{id}]");
            var response = await service.BuscarPorIdAsync(id);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Agregar")]
        [Authorize(Roles = Constantes.RolAdmin)]
        public async Task<IActionResult> AgregarAsync(TituloAddDtoRequest request)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(request)}]");
            var response = await service.AddAsync(request);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Actualizar/{id:int}")]
        [Authorize(Roles = Constantes.RolAdmin)]
        public async Task<IActionResult> ActualizarAsync(int id, [FromBody] TituloDtoRequest request)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(request)}]");
            request.Id = id;
            var response = await service.ActualizarAsync(request);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("Eliminar{id:int}")]
        [Authorize(Roles = Constantes.RolAdmin)]
        public async Task<IActionResult> EliminarAsync(int id)
        {
            ilogger.LogInformation($"id:[{id}]");
            var response = await service.EliminarAsync(id);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }
    }
}
