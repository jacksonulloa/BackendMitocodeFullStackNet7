using AutoMapper;
using GamerSellStore.Dtos.Request;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Dtos.ResponseBase;
using GamerSellStore.Entities;
using GamerSellStore.Repositories;
using GamerSellStore.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Services.Implementations
{
    public class ClasificacionService : IClasificacionService
    {
        private readonly IClasificacionRepository repository;
        private readonly ILogger<ClasificacionService> ilogger;
        private readonly IMapper mapper;
        public ClasificacionService(IClasificacionRepository _repository, ILogger<ClasificacionService> _ilogger, IMapper _mapper)
        {
            repository = _repository;
            ilogger = _ilogger;
            mapper = _mapper;
        }

        public async Task<BaseResponseLista<ClasificacionSearchDtoResponse>> ListarAsync()
        {
            var response = new BaseResponseLista<ClasificacionSearchDtoResponse>();
            ICollection<Clasificacion> lista_origen = new List<Clasificacion>();
            List<ClasificacionSearchDtoResponse> lista_fin = new List<ClasificacionSearchDtoResponse>();
            try
            {
                lista_origen = await repository.ListarAsync();
                lista_fin = mapper.Map<List<ClasificacionSearchDtoResponse>>(lista_origen);
                response.codResp = (lista_fin.Count > 0) ? "00" : "22";
                response.descResp = (lista_fin.Count > 0) ? "Conforme" : "No se encontraron registros";
                response.Data = lista_fin;
                response.TotalPages = (lista_fin.Count > 0) ? 1 : 0;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new List<ClasificacionSearchDtoResponse>();
                response.TotalPages = 0;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseLista<ClasificacionSearchDtoResponse>> ListarAsync(string nombre)
        {
            var response = new BaseResponseLista<ClasificacionSearchDtoResponse>();
            ICollection<Clasificacion> lista_origen = new List<Clasificacion>();
            List<ClasificacionSearchDtoResponse> lista_fin = new List<ClasificacionSearchDtoResponse>();
            try
            {
                lista_origen = await repository.ListarAsync(p => p.Nombre.Contains(nombre), x => x.Nombre);
                lista_fin = mapper.Map<List<ClasificacionSearchDtoResponse>>(lista_origen);
                response.codResp = (lista_fin.Count > 0) ? "00" : "22";
                response.descResp = (lista_fin.Count > 0) ? "Conforme" : "No se encontraron registros";
                response.Data = lista_fin;
                response.TotalPages = (lista_fin.Count > 0) ? 1 : 0;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new List<ClasificacionSearchDtoResponse>();
                response.TotalPages = 0;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseSingular<ClasificacionSearchDtoResponse>> BuscarPorIdAsync(int id)
        {
            var response = new BaseResponseSingular<ClasificacionSearchDtoResponse>();
            Clasificacion? objeto_inicial = new Clasificacion();
            ClasificacionSearchDtoResponse objeto_final = new ClasificacionSearchDtoResponse();
            try
            {
                objeto_inicial = await repository.BuscarPorIdAsync(id);
                objeto_final = mapper.Map<ClasificacionSearchDtoResponse>(objeto_inicial);
                response.codResp = objeto_inicial is not null ? "00" : "22";
                response.descResp = objeto_inicial is not null ? "Conforme" : "El objeto no fue encontrado";
                response.Data = objeto_final;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = null;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponse> AddAsync(ClasificacionAddDtoRequest request)
        {
            var response = new BaseResponse();
            Clasificacion objeto_final = new Clasificacion();
            try
            {
                objeto_final = mapper.Map<Clasificacion>(request);
                await repository.AgregarAsync(objeto_final);
                response.codResp = "00";
                response.descResp = "Conforme";
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }

            return response;
        }

        public async Task<BaseResponse> ActualizarAsync(ClasificacionDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var entity = await repository.BuscarPorIdAsync(request.Id);
                if (entity is null)
                {
                    response.codResp = "22";
                    response.descResp = "No se encontro el objeto a actualizar";
                    return response;
                }
                var entity_conver = mapper.Map(request, entity);
                entity.Nombre = entity_conver.Nombre;
                entity.Estado = entity_conver.Estado;
                await repository.ActualizarAsync();
                response.codResp = "00";
                response.descResp = "Conforme";
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }

            return response;
        }

        public async Task<BaseResponse> EliminarAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await repository.EliminarAsync(id);
                response.codResp = "00";
                response.descResp = "Conforme";
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
