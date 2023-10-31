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
    public class TituloService : ITituloService
    {
        private readonly ITituloRepository repositoryTitulo;
        private readonly IClasificacionRepository repositoryClasificacion;
        private readonly IPublisherRepository repositoryPublisher;
        private readonly IGeneroRepository repositoryGenero;
        private readonly IConsolaRepository repositoryConsola;
        private readonly ILogger<TituloService> ilogger;
        private readonly IMapper mapper;
        private readonly IFileUploader fileUploader;
        public TituloService(ITituloRepository _repositoryTitulo, IConsolaRepository _repositoryConsola,
            IGeneroRepository _repositoryGenero, IClasificacionRepository _repositoryClasificacion,
            IPublisherRepository _repositoryPublisher, ILogger<TituloService> _ilogger, IMapper _mapper, 
            IFileUploader _fileUploader)
        {
            repositoryTitulo = _repositoryTitulo;
            repositoryClasificacion = _repositoryClasificacion;
            repositoryConsola = _repositoryConsola;
            repositoryGenero = _repositoryGenero;
            repositoryPublisher = _repositoryPublisher;
            ilogger = _ilogger;
            mapper = _mapper;
            fileUploader = _fileUploader;
        }

        public async Task<BaseResponseLista<TituloSearchDtoResponse>> ListarAsync(TituloBusqueda busqueda,
            CancellationToken cancellationToken = default)
        {
            var response = new BaseResponseLista<TituloSearchDtoResponse>();
            ICollection<Titulo> lista_origen = new List<Titulo>();
            List<TituloSearchDtoResponse> lista_fin = new List<TituloSearchDtoResponse>();
            try
            {
                var tupla = await repositoryTitulo
                                .ListarAsync(predicado: x => x.Nombre.Contains(busqueda.Titulo ?? String.Empty),
                                selector: y=>mapper.Map<TituloSearchDtoResponse>(y),
                                ordenadoPor: z => z.Nombre, 
                                pagina: busqueda.pagina, 
                                filas: busqueda.filas, 
                                ascendente: true, 
                                relacionadocon: "Publisher,Genero,Consola,Clasificacion"
                                ); 
                response.codResp = tupla.Coleccion.Count > 0 ? "00"  : "22";
                response.descResp = tupla.Coleccion.Count > 0 ? "Conforme" : "No se encontraron resultados para la busqueda";
                response.TotalPages = Utilitario.ObtenerTotalPaginas(tupla.Total, busqueda.filas);
                response.Data = tupla.Coleccion;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new List<TituloSearchDtoResponse>();
                response.TotalPages = 0;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseLista<TituloSearchDtoResponse>> ListarAsync()
        {
            var response = new BaseResponseLista<TituloSearchDtoResponse>();
            ICollection<Titulo> lista_origen = new List<Titulo>();
            List<TituloSearchDtoResponse> lista_fin = new List<TituloSearchDtoResponse>();
            try
            {
                lista_origen = await repositoryTitulo.ListarAsync();
                lista_fin = mapper.Map<List<TituloSearchDtoResponse>>(lista_origen);
                response.codResp = (lista_fin.Count > 0) ? "00" : "22";
                response.descResp = (lista_fin.Count > 0) ? "Conforme" : "No se encontraron registros";
                response.Data = lista_fin;
                response.TotalPages = (lista_fin.Count > 0) ? 1 : 0;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new List<TituloSearchDtoResponse>();
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseLista<TituloSearchDtoResponse>> ListarAsync(TituloSearchDtoRequest request)
        {
            request = (request is null) ? new TituloSearchDtoRequest() : request;
            bool _estado = (request.TituloEstado == 1) ? true : false;
            var response = new BaseResponseLista<TituloSearchDtoResponse>();
            ICollection<Titulo> lista_origen = new List<Titulo>();
            ICollection<TituloSearchDtoResponse> lista_fin = new List<TituloSearchDtoResponse>();
            try
            {
                //lista_origen = await repositoryTitulo.ListarAsync(p => p.Nombre.Contains(request.TituloNombre) &&
                //                                                    p.Estado == (request.TituloEstado == -1) ? p.Estado : ((request.TituloEstado == 1) ? true : false) &&
                //                                                    p.Clasificacion.Nombre.Contains(request.ClasificacionNombre) &&
                //                                                    p.Consola.Nombre.Contains(request.ConsolaNombre) &&
                //                                                    p.Clasificacion.Nombre.Contains(request.ClasificacionNombre) &&
                //                                                    p.Genero.Nombre.Contains(request.GeneroNombre),
                //                                                    x => x.Nombre);
                lista_fin = await repositoryTitulo
                                .ListarAsync(predicado: p => p.Nombre.Contains(request.TituloNombre) &&
                                                                                    p.Estado == (request.TituloEstado == -1) ? p.Estado : ((request.TituloEstado == 1) ? true : false) &&
                                                                                    p.Nombre.Contains(request.TituloNombre) &&
                                                                                    p.Clasificacion.Nombre.Contains(request.ClasificacionNombre) &&
                                                                                    p.Consola.Nombre.Contains(request.ConsolaNombre) &&
                                                                                    p.Clasificacion.Nombre.Contains(request.ClasificacionNombre) &&
                                                                                    p.Genero.Nombre.Contains(request.GeneroNombre),
                                selector: y => mapper.Map<TituloSearchDtoResponse>(y),
                                ordenadoPor: z => z.Id,
                                ascendente: true,
                                relacionadocon: "Publisher,Genero,Consola,Clasificacion"
                                );


                //lista_fin = mapper.Map<List<TituloSearchDtoResponse>>(lista_origen);
                response.codResp = (lista_fin.Count > 0) ? "00" : "22";
                response.descResp = (lista_fin.Count > 0) ? "Conforme" : "No se encontraron registros";
                response.Data = lista_fin;
                response.TotalPages = (lista_fin.Count > 0) ? 1 : 0;
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

        public async Task<BaseResponseSingular<TituloFindDtoResponse>> BuscarPorIdAsync(int id)
        {
            var response = new BaseResponseSingular<TituloFindDtoResponse>();
            Titulo? objeto_inicial = new Titulo();
            TituloFindDtoResponse objeto_final = new TituloFindDtoResponse();
            try
            {
                objeto_inicial = await repositoryTitulo.BuscarPorIdAsync(id);
                objeto_final = mapper.Map<TituloFindDtoResponse>(objeto_inicial);
                response.codResp = objeto_inicial is not null ? "00" : "22";
                response.descResp = objeto_inicial is not null ? "Conforme" : "El objeto no fue encontrado";
                response.Data = objeto_final;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new TituloFindDtoResponse();
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponse> AddAsync(TituloAddDtoRequest request)
        {
            var response = new BaseResponse();
            Titulo objeto_final = new Titulo();
            try
            {
                ICollection<Titulo> lista_titulo = new List<Titulo>();
                lista_titulo = await repositoryTitulo.ListarAsync(p => p.Nombre == request.Nombre, x => x.Nombre);
                if (lista_titulo.Count > 0)
                {
                    response.codResp = "99";
                    response.descResp = "El titulo ya se encuentra registrado";
                    return response;
                }

                Genero? genero = await repositoryGenero.BuscarPorIdAsync(request.GeneroId);
                if (genero is null)
                {
                    response.codResp = "99";
                    response.descResp = "El objeto genero no se encuentra registrado";
                    return response;
                }
                else if (!genero.Estado)
                {
                    response.codResp = "99";
                    response.descResp = "El objeto genero no puede ser asignado dado que tiene estado inactivo";
                    return response;
                }
                Clasificacion? clasificacion = await repositoryClasificacion.BuscarPorIdAsync(request.ClasificacionId);
                if (clasificacion is null)
                {
                    response.codResp = "99";
                    response.descResp = "El objeto clasificacion no se encuentra registrado";
                    return response;
                }
                else if (!clasificacion.Estado)
                {
                    response.codResp = "99";
                    response.descResp = "El objeto clasificacion no puede ser asignado dado que tiene estado inactivo";
                    return response;
                }
                Publisher? publisher = await repositoryPublisher.BuscarPorIdAsync(request.PublisherId);
                if (publisher is null)
                {
                    response.codResp = "99";
                    response.descResp = "El objeto publisher no se encuentra registrado";
                    return response;
                }
                else if (!publisher.Estado)
                {
                    response.codResp = "99";
                    response.descResp = "El objeto publisher no puede ser asignado dado que tiene estado inactivo";
                    return response;
                }
                Consola? consola = await repositoryConsola.BuscarPorIdAsync(request.ConsolaId);
                if (consola is null)
                {
                    response.codResp = "99";
                    response.descResp = "El objeto consola no se encuentra registrado";
                    return response;
                }
                else if (!consola.Estado)
                {
                    response.codResp = "99";
                    response.descResp = "El objeto consola no puede ser asignado dado que tiene estado inactivo";
                    return response;
                }
                objeto_final = mapper.Map<Titulo>(request);
                objeto_final.ImageUrl = await fileUploader.CargarImagenAsync(request.Base64Image, request.NombreArchivo);
                objeto_final.Publisher = publisher;
                objeto_final.Clasificacion = clasificacion;
                objeto_final.Genero = genero;
                objeto_final.Consola = consola;
                await repositoryTitulo.AgregarAsync(objeto_final);
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

        public async Task<BaseResponse> ActualizarAsync(TituloDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var entity = await repositoryTitulo.BuscarPorIdAsync(request.Id);
                if (entity is null)
                {
                    response.codResp = "22";
                    response.descResp = "No se encontro el objeto a actualizar";
                    return response;
                }
                var entity_conver = mapper.Map(request, entity);
                entity.Nombre = entity_conver.Nombre;
                entity.Descripcion = entity_conver.Descripcion;
                entity.PublisherId = entity_conver.PublisherId;
                entity.GeneroId = entity_conver.GeneroId;
                entity.ConsolaId = entity_conver.ConsolaId;
                entity.Costo = entity_conver.Costo;
                entity.Moneda = entity_conver.Moneda;
                entity.Stock = entity_conver.Stock;
                entity.Agotado = entity_conver.Agotado;
                entity.ImageUrl = entity_conver.ImageUrl;
                entity.ClasificacionId = entity_conver.ClasificacionId;
                entity.AnioPublicacion = entity_conver.AnioPublicacion;
                entity.Estado = entity_conver.Estado;
                if(!string.IsNullOrEmpty(request.Base64Image))
                {
                    entity.ImageUrl = await fileUploader.CargarImagenAsync(request.Base64Image, request.NombreArchivo);
                }
                await repositoryTitulo.ActualizarAsync();
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
                await repositoryTitulo.EliminarAsync(id);
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
