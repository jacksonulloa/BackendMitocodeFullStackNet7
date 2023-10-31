using GamerSellStore.Dtos.Request;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Dtos.ResponseBase;

namespace GamerSellStore.Services.Interfaces
{
    public interface IConsolaService
    {
        Task<BaseResponse> ActualizarAsync(ConsolaDtoRequest request);
        Task<BaseResponse> AddAsync(ConsolaAddDtoRequest request);
        Task<BaseResponseSingular<ConsolaSearchDtoResponse>> BuscarPorIdAsync(int id);
        Task<BaseResponse> EliminarAsync(int id);
        Task<BaseResponseLista<ConsolaSearchDtoResponse>> ListarAsync();

        Task<BaseResponseLista<ConsolaSearchDtoResponse>> ListarAsync(string nombre);
    }
}