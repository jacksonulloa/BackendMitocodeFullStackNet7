using GamerSellStore.Dtos.Request;
using GamerSellStore.Entities;
using GamerSellStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GamerSellStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClasificacionController : ControllerBase
    {
        private readonly IClasificacionService service;
        private readonly ILogger<ClasificacionController> ilogger;

        public ClasificacionController(IClasificacionService _service, ILogger<ClasificacionController> _ilogger)
        {
            service = _service;
            ilogger = _ilogger;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarAsync()
        {            
            var response = await service.ListarAsync();
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest();
        }

        [HttpGet("ListarPorNombre")]
        public async Task<IActionResult> ListarPorNombreAsync([FromQuery] String publisher = "")
        {
            ilogger.LogInformation($"publisher:[{publisher}]");
            var response = await service.ListarAsync(publisher);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest();
        }

        [HttpGet("BuscarPorId")]
        public async Task<IActionResult> BuscarPorIdAsync(int id)
        {
            ilogger.LogInformation($"id:[{id}]");
            var response = await service.BuscarPorIdAsync(id);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest();
        }

        [HttpPost("Agregar")]
        public async Task<IActionResult> AgregarAsync(ClasificacionAddDtoRequest objeto)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(objeto)}]");
            var response = await service.AddAsync(objeto);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest();
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> ActualizarAsync([FromQuery] ClasificacionDtoRequest objeto)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(objeto)}]");
            var response = await service.ActualizarAsync(objeto);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest();
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> EliminarAsync(int id)
        {
            ilogger.LogInformation($"id:[{id}]");
            var response = await service.EliminarAsync(id);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest();
        }
    }
}
