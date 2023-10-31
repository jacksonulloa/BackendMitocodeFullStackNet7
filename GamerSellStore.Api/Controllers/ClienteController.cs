using GamerSellStore.Dtos.Request;
using GamerSellStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GamerSellStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClienteController:ControllerBase
    {
        private readonly IClienteService service;
        private readonly ILogger<ClienteController> ilogger;

        public ClienteController(IClienteService _service, ILogger<ClienteController> _ilogger)
        {
            service = _service;
            ilogger = _ilogger;
        }

        [HttpGet("Listar")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarAsync()
        {
            var response = await service.ListarAsync();
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ListarPorNombrePais")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarPorNombrePaisAsync([FromQuery] String nombre = "", [FromQuery] string pais = "")
        {
            ilogger.LogInformation($"Nombre:[{nombre}], Pais:[{pais}]");
            var response = await service.ListarAsync(nombre, pais);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("BuscarPorId")]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarPorIdAsync(int id)
        {
            ilogger.LogInformation($"id:[{id}]");
            var response = await service.BuscarPorIdAsync(id);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpPost("Agregar")]
        public async Task<IActionResult> AgregarAsync(ClienteAddDtoRequest objeto)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(objeto)}]");
            var response = await service.AddAsync(objeto);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> ActualizarAsync([FromQuery] ClienteDtoRequest objeto)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(objeto)}]");
            var response = await service.ActualizarAsync(objeto);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> EliminarAsync(int id)
        {
            ilogger.LogInformation($"id:[{id}]");
            var response = await service.EliminarAsync(id);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }
    }
}
