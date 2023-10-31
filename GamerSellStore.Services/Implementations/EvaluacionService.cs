using AutoMapper;
using GamerSellStore.Dtos.Request;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Dtos.ResponseBase;
using GamerSellStore.Entities;
using GamerSellStore.Entities.Info;
using GamerSellStore.Repositories;
using GamerSellStore.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Services.Implementations
{
    public class EvaluacionService : IEvaluacionService
    {
        private readonly IEvaluacionRepository repositoryEvaluacion;
        private readonly IClienteRepository repositoryCliente;
        private readonly ITituloRepository repositoryTitulo;
        private readonly ILogger<EvaluacionService> ilogger;
        private readonly IMapper mapper;

        public EvaluacionService(IEvaluacionRepository _repositoryEvaluacion, ILogger<EvaluacionService> _ilogger, IMapper _mapper,
            IClienteRepository _repositoryCliente, ITituloRepository _repositoryTitulo)
        {
            repositoryEvaluacion = _repositoryEvaluacion;
            ilogger = _ilogger;
            mapper = _mapper;
            repositoryCliente = _repositoryCliente;
            repositoryTitulo = _repositoryTitulo;
        }

        public async Task<BaseResponseLista<EvaluacionDtoResponse>> ListarAsync(EvaluacionBusqueda busqueda,
            CancellationToken cancellationToken = default)
        {
            BaseResponseLista<EvaluacionDtoResponse> response = new BaseResponseLista<EvaluacionDtoResponse>();
            ICollection<Evaluacion> lista_origen = new List<Evaluacion>();
            List<EvaluacionDtoResponse> lista_fin = new List<EvaluacionDtoResponse>();
            try
            {
                DateTime Desde = Convert.ToDateTime(busqueda.Desde);
                DateTime Hasta = Convert.ToDateTime(busqueda.Hasta);
                var tupla = await repositoryEvaluacion
                                .ListarAsync(predicado: x => x.Fecha >= Desde && x.Fecha <= Hasta &&
                                    x.Cliente.Nombre.Contains(busqueda.cliente ?? String.Empty) &&
                                    x.Titulo.Nombre.Contains(busqueda.titulo ?? String.Empty),
                                selector: y => mapper.Map<EvaluacionDtoResponse>(y),
                                ordenadoPor: z => z.Fecha,
                                pagina: busqueda.pagina,
                                filas: busqueda.filas,
                                ascendente: true,
                                relacionadocon: "Titulo,Cliente"
                                );
                response.codResp = tupla.Coleccion.Count > 0 ? "00" : "22";
                response.descResp = tupla.Coleccion.Count > 0 ? "Conforme" : "No se encontraron resultados para la busqueda";
                response.TotalPages = Utilitario.ObtenerTotalPaginas(tupla.Total, busqueda.filas);
                response.Data = tupla.Coleccion;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new List<EvaluacionDtoResponse>();
                response.TotalPages = 0;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseLista<EvaluacionInfo>> ListarAsync(EvaluacionInfoDtoRequest request)
        {
            var response = new BaseResponseLista<EvaluacionInfo>();
            ICollection<EvaluacionInfo> lista_final = new List<EvaluacionInfo>();
            try
            {
                lista_final = await repositoryEvaluacion.ListarInfoAsync(request.Titulo, request.Publisher, request.Genero, request.Consola, default);
                response.codResp = (lista_final.Count > 0) ? "00" : "22";
                response.descResp = (lista_final.Count > 0) ? "Conforme" : "No se encontraron registros";
                response.Data = lista_final;
                response.TotalPages = (lista_final.Count > 0) ? 1 : 0;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new List<EvaluacionInfo>();
                response.TotalPages = 0;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseId> AddAsync(string Email, EvaluacionAddDtoRequest request)
        {
            var response = new BaseResponseId();
            try
            {
                await repositoryEvaluacion.CrearTransaccionAsync();

                var evaluacion = mapper.Map<Evaluacion>(request);

                var cliente = await repositoryCliente.BuscarPorEmailAsync(request.Email);
                if (cliente is null)
                {
                    response.codResp = "99";
                    response.descResp = "El cliente no se encuentra registrado";
                    response.Id = 0;
                    await repositoryEvaluacion.RollbackTransaccionAsync();
                    return response;
                }
                else if (!cliente.Estado)
                {
                    response.codResp = "99";
                    response.descResp = "El cliente no se encuentra activo";
                    response.Id = 0;
                    await repositoryEvaluacion.RollbackTransaccionAsync();
                    return response;
                }
                var titulo = await repositoryTitulo.BuscarPorIdAsync(request.TituloId);
                if (titulo is null)
                {
                    response.codResp = "99";
                    response.descResp = "El titulo no se encuentra registrado";
                    response.Id = 0;
                    await repositoryEvaluacion.RollbackTransaccionAsync();
                    return response;
                }
                else if (!titulo.Estado)
                {
                    response.codResp = "99";
                    response.descResp = "El titulo no se encuentra disponible";
                    response.Id = 0;
                    await repositoryEvaluacion.RollbackTransaccionAsync();
                    return response;
                }
                evaluacion.Cliente = cliente;
                evaluacion.ClienteId = cliente.Id;
                evaluacion.Titulo = titulo;
                evaluacion.TituloId = titulo.Id;
                evaluacion.Calificacion = request.Calificacion;
                evaluacion.Resenia = request.Resenia;
                int idEvaluacion = await repositoryEvaluacion.AgregarAsync(evaluacion);
                await repositoryEvaluacion.ActualizarAsync();
                response.codResp = "00";
                response.descResp = "Conforme";
                response.Id = idEvaluacion;
            }
            catch (Exception exc)
            {
                await repositoryEvaluacion.RollbackTransaccionAsync();
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Id = 0;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseSingular<EvaluacionDtoResponse>> BuscarPorIdAsync(int id)
        {
            BaseResponseSingular<EvaluacionDtoResponse> response = new BaseResponseSingular<EvaluacionDtoResponse>();
            try
            {
                var evaluacion = await repositoryEvaluacion.BuscarPorIdAsync(id);
                response.codResp = evaluacion is null ? "99" : "00";
                response.descResp = evaluacion is null ? "El objeto evalucion no se encuentra registrado" : "Conforme";
                response.Data = evaluacion is null ? default : mapper.Map<EvaluacionDtoResponse>(evaluacion);
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = default;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }


    }
}
