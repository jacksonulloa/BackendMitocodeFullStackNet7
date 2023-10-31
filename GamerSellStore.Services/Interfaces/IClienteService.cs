using GamerSellStore.Dtos.Request;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Dtos.ResponseBase;

namespace GamerSellStore.Services.Interfaces
{
    public interface IClienteService
    {
        Task<BaseResponse> ActualizarAsync(ClienteDtoRequest request);
        Task<BaseResponse> AddAsync(ClienteAddDtoRequest request);
        Task<BaseResponseSingular<ClienteSearchDtoResponse>> BuscarPorIdAsync(int id);
        Task<BaseResponse> EliminarAsync(int id);
        Task<BaseResponseLista<ClienteSearchDtoResponse>> ListarAsync();
        Task<BaseResponseLista<ClienteSearchDtoResponse>> ListarAsync(string nombre, string pais);
    }
}