using GamerSellStore.Dtos.Request;
using GamerSellStore.Entities;
using GamerSellStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GamerSellStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController:ControllerBase
    {
        private readonly IReservaService service;
        private readonly ILogger<ReservaController> ilogger;

        public ReservaController(IReservaService _service, ILogger<ReservaController> _ilogger)
        {
            service = _service;
            ilogger = _ilogger;
        }

        [HttpGet("BuscarPorId")]
        [Authorize]
        public async Task<IActionResult> BuscarPorIdAsync(int id)
        {
            ilogger.LogInformation($"id:[{id}]");
            var response = await service.BuscarPorIdAsync(id);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest();
        }

        [HttpGet("ReporteReserva")]
        [Authorize(Roles = Constantes.RolAdmin)]
        public async Task<IActionResult> ReporteReservaAsync([FromQuery] ReservaReporteDtoRequest request)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(request)}]");
            var response = await service.ReporteReservaAsync(request);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest();
        }

        [HttpGet("ListarPaginado")]
        [Authorize]
        public async Task<IActionResult> ListarPaginadoAsync([FromQuery] ReservaBusqueda request, CancellationToken cancellationToken = default)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(request)}]");
            var response = await service.ListarAsync(request, cancellationToken);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest();
        }

        [HttpPost("Agregar")]
        [Authorize(Roles = Constantes.RolAdmin)]
        public async Task<IActionResult> AgregarAsync([FromBody]ReservaAddDtoRequest objeto, String Email)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(objeto)}], Email:[{Email}]");
            var response = await service.AddAsync(Email, objeto);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest();
        }
    }
}
