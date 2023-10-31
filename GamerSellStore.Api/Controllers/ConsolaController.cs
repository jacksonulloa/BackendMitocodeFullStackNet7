using GamerSellStore.Dtos.Request;
using GamerSellStore.Entities;
using GamerSellStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GamerSellStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = Constantes.RolAdmin)]
    public class ConsolaController : ControllerBase
    {
        private readonly IConsolaService service;
        private readonly ILogger<GeneroController> ilogger;

        public ConsolaController(IConsolaService _service, ILogger<GeneroController> _ilogger)
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

        [HttpGet("ListarPorNombre")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarPorNombreAsync([FromQuery] string consola = "")
        {
            ilogger.LogInformation($"Consola:[{consola}]");
            var response = await service.ListarAsync(consola);
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
        public async Task<IActionResult> AgregarAsync(ConsolaAddDtoRequest objeto)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(objeto)}]");
            var response = await service.AddAsync(objeto);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> ActualizarAsync([FromQuery] ConsolaDtoRequest objeto)
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
