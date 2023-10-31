using GamerSellStore.Dtos.Request;
using GamerSellStore.Entities;
using GamerSellStore.Services.Implementations;
using GamerSellStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GamerSellStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService service;
        private readonly ILogger<PublisherController> ilogger;

        public PublisherController(IPublisherService _service, ILogger<PublisherController> _ilogger)
        {
            service = _service;
            ilogger = _ilogger;
        }

        [HttpGet("Listar")]
        //[AllowAnonymous]
        public async Task<IActionResult> ListarAsync()
        {
            var response = await service.ListarAsync();
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ListarPorNombre")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarPorNombreAsync([FromQuery] string publisher = "")
        {
            ilogger.LogInformation($"Publisher:[{publisher}]");
            var response = await service.ListarAsync(publisher);
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
        public async Task<IActionResult> AgregarAsync(PublisherAddDtoRequest objeto)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(objeto)}]");
            var response = await service.AddAsync(objeto);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Actualizar")]
        [Authorize(Roles = Constantes.RolAdmin)]
        public async Task<IActionResult> ActualizarAsync([FromQuery] PublisherDtoRequest objeto)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(objeto)}]");
            var response = await service.ActualizarAsync(objeto);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("Eliminar/{id}")]
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
