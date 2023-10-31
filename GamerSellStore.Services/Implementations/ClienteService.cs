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
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository repository;
        private readonly ILogger<ClienteService> ilogger;
        private readonly IMapper mapper;
        public ClienteService(IClienteRepository _repository, ILogger<ClienteService> _ilogger, IMapper _mapper)
        {
            repository = _repository;
            ilogger = _ilogger;
            mapper = _mapper;
        }

        public async Task<BaseResponseLista<ClienteSearchDtoResponse>> ListarAsync()
        {
            var response = new BaseResponseLista<ClienteSearchDtoResponse>();
            ICollection<Cliente> lista_origen = new List<Cliente>();
            List<ClienteSearchDtoResponse> lista_fin = new List<ClienteSearchDtoResponse>();
            try
            {
                lista_origen = await repository.ListarAsync();
                lista_fin = mapper.Map<List<ClienteSearchDtoResponse>>(lista_origen);
                response.codResp = (lista_fin.Count > 0) ? "00" : "22";
                response.descResp = (lista_fin.Count > 0) ? "Conforme" : "No se encontraron registros";
                response.Data = lista_fin;
                response.TotalPages = (lista_fin.Count > 0) ? 1 : 0;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new List<ClienteSearchDtoResponse>();
                response.TotalPages = 0;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseLista<ClienteSearchDtoResponse>> ListarAsync(string nombre, string pais)
        {
            var response = new BaseResponseLista<ClienteSearchDtoResponse>();
            ICollection<Cliente> lista_origen = new List<Cliente>();
            List<ClienteSearchDtoResponse> lista_fin = new List<ClienteSearchDtoResponse>();
            try
            {
                lista_origen = await repository.ListarAsync(p => p.Nombre.Contains(nombre) && p.Pais.Contains(pais), x => x.Nombre);
                lista_fin = mapper.Map<List<ClienteSearchDtoResponse>>(lista_origen);
                response.codResp = (lista_fin.Count > 0) ? "00" : "22";
                response.descResp = (lista_fin.Count > 0) ? "Conforme" : "No se encontraron registros";
                response.Data = lista_fin;
                response.TotalPages = (lista_fin.Count > 0) ? 1 : 0;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new List<ClienteSearchDtoResponse>();
                response.TotalPages = 0;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseSingular<ClienteSearchDtoResponse>> BuscarPorIdAsync(int id)
        {
            var response = new BaseResponseSingular<ClienteSearchDtoResponse>();
            Cliente? objeto_inicial = new Cliente();
            ClienteSearchDtoResponse objeto_final = new ClienteSearchDtoResponse();
            try
            {
                objeto_inicial = await repository.BuscarPorIdAsync(id);
                objeto_final = mapper.Map<ClienteSearchDtoResponse>(objeto_inicial);
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

        public async Task<BaseResponse> AddAsync(ClienteAddDtoRequest request)
        {
            var response = new BaseResponse();
            Cliente objeto_final = new Cliente();
            try
            {
                objeto_final = mapper.Map<Cliente>(request);
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

        public async Task<BaseResponse> ActualizarAsync(ClienteDtoRequest request)
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
                entity.Correo = entity_conver.Correo;
                entity.Usuario = entity_conver.Usuario;
                entity.Sexo = entity_conver.Sexo;
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
