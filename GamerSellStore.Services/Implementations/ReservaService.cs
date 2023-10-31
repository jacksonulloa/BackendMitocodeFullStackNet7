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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Services.Implementations
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository repositoryReserva;
        private readonly IClienteRepository repositoryCliente;
        private readonly ITituloRepository repositoryTitulo;
        private readonly ILogger<ReservaService> ilogger;
        private readonly IMapper mapper;
        
        public ReservaService(IReservaRepository _repositoryReserva, ILogger<ReservaService> _ilogger, IMapper _mapper,
        IClienteRepository _repositoryCliente, ITituloRepository _repositoryTitulo)
        {
            repositoryReserva = _repositoryReserva;
            ilogger = _ilogger;
            mapper = _mapper;
            repositoryCliente = _repositoryCliente;
            repositoryTitulo = _repositoryTitulo;
        }

        public async Task<BaseResponseLista<ReservaDtoResponse>> ListarAsync(ReservaBusqueda busqueda,
            CancellationToken cancellationToken = default)
        {
            BaseResponseLista<ReservaDtoResponse> response = new BaseResponseLista<ReservaDtoResponse>();
            ICollection<Reserva> lista_origen = new List<Reserva>();
            List<ReservaDtoResponse> lista_fin = new List<ReservaDtoResponse>();
            try
            {
                DateTime Desde = Convert.ToDateTime(busqueda.Desde);
                DateTime Hasta = Convert.ToDateTime(busqueda.Hasta);
                var tupla = await repositoryReserva
                                .ListarAsync(predicado: x => x.FechaTxn >= Desde && x.FechaTxn <= Hasta &&
                                    x.Cliente.Nombre.Contains(busqueda.cliente ?? String.Empty) &&
                                    x.Titulo.Nombre.Contains(busqueda.titulo ?? String.Empty),
                                selector: y => mapper.Map<ReservaDtoResponse>(y),
                                ordenadoPor: z => z.FechaTxn,
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
            catch(Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new List<ReservaDtoResponse>();
                response.TotalPages = 0;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseId> AddAsync(string Email, ReservaAddDtoRequest request)
        {
            var response = new BaseResponseId();
            try
            {
                await repositoryReserva.CrearTransaccionAsync();

                var reserva = mapper.Map<Reserva>(request);

                var cliente = await repositoryCliente.BuscarPorEmailAsync(request.Email);
                if (cliente is null)
                {
                    response.codResp = "99";
                    response.descResp = "El cliente no se encuentra registrado";
                    response.Id = 0;
                    await repositoryReserva.RollbackTransaccionAsync();
                    return response;
                }
                else if (!cliente.Estado)
                {
                    response.codResp = "99";
                    response.descResp = "El cliente no se encuentra activo";
                    response.Id = 0;
                    await repositoryReserva.RollbackTransaccionAsync();
                    return response;
                }
                var titulo = await repositoryTitulo.BuscarPorIdAsync(request.TituloId);
                if (titulo is null)
                {
                    response.codResp = "99";
                    response.descResp = "El titulo no se encuentra registrado";
                    response.Id = 0;
                    await repositoryReserva.RollbackTransaccionAsync();
                    return response;
                }
                else if (!titulo.Estado)
                {
                    response.codResp = "99";
                    response.descResp = "El titulo no se encuentra disponible";
                    response.Id = 0;
                    await repositoryReserva.RollbackTransaccionAsync();
                    return response;
                }
                else if (titulo.Stock == 0)
                {
                    response.codResp = "99";
                    response.descResp = "El titulo se encuentra agotado";
                    response.Id = 0;
                    await repositoryReserva.RollbackTransaccionAsync();
                    return response;
                }
                else if (titulo.Stock < request.CantidadReserva)
                {
                    response.codResp = "99";
                    response.descResp = "La cantidad de reserva supera al stock disponible";
                    response.Id = 0;
                    await repositoryReserva.RollbackTransaccionAsync();
                    return response;
                }
                reserva.Cliente = cliente;
                reserva.ClienteId = cliente.Id;
                reserva.Titulo = titulo;
                reserva.TituloId = titulo.Id;
                reserva.Cantidad = request.CantidadReserva;
                reserva.FechaTxn = DateTime.Now;
                reserva.Estado = true;
                reserva.NroTxn = Utilitario.GenerarOperacion(reserva.FechaTxn);
                reserva.Moneda = titulo.Moneda;
                reserva.Estado = true;
                reserva.PrecioUnitario = titulo.Costo;
                reserva.ImporteTotal = reserva.PrecioUnitario * reserva.Cantidad;
                int idCliente = await repositoryReserva.AgregarAsync(reserva);
                titulo.Stock = titulo.Stock - reserva.Cantidad;
                titulo.Agotado = titulo.Stock == 0 ? 1 : 0;
                await repositoryReserva.ActualizarAsync();
                await repositoryTitulo.ActualizarAsync();
                response.codResp = "00";
                response.descResp = "Conforme";
                response.Id = idCliente;
            }
            catch (Exception exc)
            {
                await repositoryReserva.RollbackTransaccionAsync();
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Id = 0;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }

        public async Task<BaseResponseSingular<ReservaDtoResponse>> BuscarPorIdAsync(int id)
        {
            BaseResponseSingular<ReservaDtoResponse> response = new BaseResponseSingular<ReservaDtoResponse>();
            try
            {
                // Codigo
                var reserva = await repositoryReserva.BuscarPorIdAsync(id);
                response.codResp = reserva is null ? "99" : "00";
                response.descResp = reserva is null ? "El objeto reserva no se encuentra registrado" : "Conforme";
                response.Data = reserva is null ? default : mapper.Map<ReservaDtoResponse>(reserva);
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

        public async Task<BaseResponseLista<ReservaInfo>> ReporteReservaAsync(ReservaReporteDtoRequest request)
        {
            BaseResponseLista<ReservaInfo> response = new BaseResponseLista<ReservaInfo>();
            ICollection<ReservaInfo> lista_final = new List<ReservaInfo>();
            try
            {
                var desde = Convert.ToDateTime(request.Desde);
                var hasta = Convert.ToDateTime(request.Hasta);

                lista_final = await repositoryReserva.ReporteReservaAsync(desde, hasta);
                response.codResp = (lista_final.Count > 0) ? "00" : "22";
                response.descResp = (lista_final.Count > 0) ? "Conforme" : "No se encontraron resultados";
                response.Data = lista_final;
                response.TotalPages = (lista_final.Count > 0) ? 1 : 0;
            }
            catch (Exception exc)
            {
                response.codResp = "99";
                response.descResp = $"Error: {exc.Message}";
                response.Data = new List<ReservaInfo>();
                response.TotalPages = 0;
                ilogger.LogError(exc, "Error: {Message}", exc.Message);
            }
            return response;
        }
    }
}
