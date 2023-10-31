using GamerSellStore.Dtos.Request;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Dtos.ResponseBase;
using GamerSellStore.Entities.Info;

namespace GamerSellStore.Services.Interfaces
{
    public interface IEvaluacionService
    {
        Task<BaseResponseLista<EvaluacionDtoResponse>> ListarAsync(EvaluacionBusqueda busqueda,
            CancellationToken cancellationToken = default);
        Task<BaseResponseId> AddAsync(string Email, EvaluacionAddDtoRequest request);
        Task<BaseResponseSingular<EvaluacionDtoResponse>> BuscarPorIdAsync(int id);
        Task<BaseResponseLista<EvaluacionInfo>> ListarAsync(EvaluacionInfoDtoRequest request);
    }
}