using GamerSellStore.Dtos.Request;
using GamerSellStore.Entities;
using GamerSellStore.Repositories;
using GamerSellStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GamerSellStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = Constantes.RolAdmin)]
    public class EvaluacionController : ControllerBase
    {
        private readonly IEvaluacionService service;
        private readonly ILogger<EvaluacionController> ilogger;

        public EvaluacionController(IEvaluacionService _service, ILogger<EvaluacionController> _ilogger)
        {
            service = _service;
            ilogger = _ilogger;
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
        [Authorize]
        public async Task<IActionResult> AgregarAsync([FromBody] EvaluacionAddDtoRequest objeto, String Email)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(objeto)}], Email:[{Email}]");
            var response = await service.AddAsync(Email, objeto);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ListarInfo")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarInfo([FromQuery] EvaluacionInfoDtoRequest request)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(request)}]");
            var response = await service.ListarAsync(request);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ListarPaginado")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarPaginadoAsync([FromQuery] EvaluacionBusqueda request, CancellationToken cancellationToken = default)
        {
            ilogger.LogInformation($"REQUEST:[{JsonConvert.SerializeObject(request)}]");
            var response = await service.ListarAsync(request, cancellationToken);
            ilogger.LogInformation($"RESPONSE:[{JsonConvert.SerializeObject(response)}]");
            return response is not null ? Ok(response) : BadRequest(response);
        }
    }
}
