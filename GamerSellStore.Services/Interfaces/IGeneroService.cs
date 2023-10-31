using GamerSellStore.Dtos.Request;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Dtos.ResponseBase;

namespace GamerSellStore.Services.Interfaces
{
    public interface IGeneroService
    {
        Task<BaseResponse> ActualizarAsync(GeneroDtoRequest request);
        Task<BaseResponse> AddAsync(GeneroAddDtoRequest request);
        Task<BaseResponseSingular<GeneroSearchDtoResponse>> BuscarPorIdAsync(int id);
        Task<BaseResponse> EliminarAsync(int id);
        Task<BaseResponseLista<GeneroSearchDtoResponse>> ListarAsync();
    }
}