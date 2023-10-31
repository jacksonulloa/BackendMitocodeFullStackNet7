using GamerSellStore.Dtos.Request;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Dtos.ResponseBase;
using GamerSellStore.Entities.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Services.Interfaces
{
    public interface IReservaService
    {
        Task<BaseResponseLista<ReservaDtoResponse>> ListarAsync(ReservaBusqueda busqueda,
            CancellationToken cancellationToken = default);
        Task<BaseResponseId> AddAsync(string Email, ReservaAddDtoRequest request);
        Task<BaseResponseSingular<ReservaDtoResponse>> BuscarPorIdAsync(int id);
        Task<BaseResponseLista<ReservaInfo>> ReporteReservaAsync(ReservaReporteDtoRequest request);
    }
}
