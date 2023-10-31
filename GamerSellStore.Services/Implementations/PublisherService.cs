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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Services.Implementations
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository repository;
        private readonly ILogger<PublisherService> ilogger;
        private readonly IMapper mapper;
        public PublisherService(IPublisherRepository _repository, ILogger<PublisherService> _ilogger, IMapper _mapper)
        {
            repository = _repository;
            ilogger = _ilogger;
            mapper = _mapper;
        }

        public async Task<BaseResponseLista<PublisherSearchDtoResponse>> ListarAsync()
        {
            var response = new BaseResponseLista<PublisherSearchDtoResponse>();
            ICollection<Publisher> lista_origen = new List<Publisher>();
            List<PublisherSearchDtoResponse> lista_fin = new List<PublisherSearchDtoResponse>();
            try
            {
                lista_origen = await repository.ListarAsync();
                lista_fin = mapper.Map<List<PublisherSearchDtoResponse>>(lista_origen);
                response.codResp = (lista_fin.Count > 0) ? "00" : "22";
                response.descResp = (lista_fin.Count > 0) ? "Conforme" : "No se encontraron registros";
                response.Data = lista_fin;
                response.TotalPages = (lista_fin.Count > 0) ? 1 : 0;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new List<PublisherSearchDtoResponse>();
                response.TotalPages = 0;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseLista<PublisherSearchDtoResponse>> ListarAsync(string nombre)
        {
            var response = new BaseResponseLista<PublisherSearchDtoResponse>();
            ICollection<Publisher> lista_origen = new List<Publisher>();
            List<PublisherSearchDtoResponse> lista_fin = new List<PublisherSearchDtoResponse>();
            try
            {
                lista_origen = await repository.ListarAsync(p => p.Nombre.Contains(nombre), x => x.Nombre);
                lista_fin = mapper.Map<List<PublisherSearchDtoResponse>>(lista_origen);
                response.codResp = (lista_fin.Count > 0) ? "00" : "22";
                response.descResp = (lista_fin.Count > 0) ? "Conforme" : "No se encontraron registros";
                response.Data = lista_fin;
                response.TotalPages = (lista_fin.Count > 0) ? 1 : 0;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new List<PublisherSearchDtoResponse>();
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseSingular<PublisherSearchDtoResponse>> BuscarPorIdAsync(int id)
        {
            var response = new BaseResponseSingular<PublisherSearchDtoResponse>();
            Publisher? objeto_inicial = new Publisher();
            PublisherSearchDtoResponse objeto_final = new PublisherSearchDtoResponse();
            try
            {
                objeto_inicial = await repository.BuscarPorIdAsync(id);
                objeto_final = mapper.Map<PublisherSearchDtoResponse>(objeto_inicial);
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

        public async Task<BaseResponse> AddAsync(PublisherAddDtoRequest request)
        {
            var response = new BaseResponse();
            Publisher objeto_final = new Publisher();
            try
            {
                objeto_final = mapper.Map<Publisher>(request);
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

        public async Task<BaseResponse> ActualizarAsync(PublisherDtoRequest request)
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
                entity.Pais = entity_conver.Pais;
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
