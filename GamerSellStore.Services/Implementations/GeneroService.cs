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
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository repository;
        private readonly ILogger<GeneroService> ilogger;
        private readonly IMapper mapper;
        public GeneroService(IGeneroRepository _repository, ILogger<GeneroService> _ilogger, IMapper _mapper)
        {
            repository = _repository;
            ilogger = _ilogger;
            mapper = _mapper;
        }

        public async Task<BaseResponseLista<GeneroSearchDtoResponse>> ListarAsync()
        {
            var response = new BaseResponseLista<GeneroSearchDtoResponse>();
            ICollection<Genero> lista_origen = new List<Genero>();
            List<GeneroSearchDtoResponse> lista_fin = new List<GeneroSearchDtoResponse>();
            try
            {
                lista_origen = await repository.ListarAsync();
                lista_fin = mapper.Map<List<GeneroSearchDtoResponse>>(lista_origen);
                response.codResp = (lista_fin.Count > 0) ? "00" : "22";
                response.descResp = (lista_fin.Count > 0) ? "Conforme" : "No se encontraron registros";
                response.Data = lista_fin;
                response.TotalPages = (lista_fin.Count > 0) ? 1 : 0;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new List<GeneroSearchDtoResponse>();
                response.TotalPages = 0;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseSingular<GeneroSearchDtoResponse>> BuscarPorIdAsync(int id)
        {
            var response = new BaseResponseSingular<GeneroSearchDtoResponse>();
            Genero? objeto_inicial = new Genero();
            GeneroSearchDtoResponse objeto_final = new GeneroSearchDtoResponse();
            try
            {
                objeto_inicial = await repository.BuscarPorIdAsync(id);
                objeto_final = mapper.Map<GeneroSearchDtoResponse>(objeto_inicial);
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

        public async Task<BaseResponse> AddAsync(GeneroAddDtoRequest request)
        {
            var response = new BaseResponse();
            Genero objeto_final = new Genero();
            try
            {
                objeto_final = mapper.Map<Genero>(request);
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

        public async Task<BaseResponse> ActualizarAsync(GeneroDtoRequest request)
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
                mapper.Map(request, entity);
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
