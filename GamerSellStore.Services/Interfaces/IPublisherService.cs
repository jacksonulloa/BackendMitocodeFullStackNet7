using GamerSellStore.Dtos.Request;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Dtos.ResponseBase;

namespace GamerSellStore.Services.Interfaces
{
    public interface IPublisherService
    {
        Task<BaseResponse> ActualizarAsync(PublisherDtoRequest request);
        Task<BaseResponse> AddAsync(PublisherAddDtoRequest request);
        Task<BaseResponseSingular<PublisherSearchDtoResponse>> BuscarPorIdAsync(int id);
        Task<BaseResponse> EliminarAsync(int id);
        Task<BaseResponseLista<PublisherSearchDtoResponse>> ListarAsync();
        Task<BaseResponseLista<PublisherSearchDtoResponse>> ListarAsync(string nombre);
    }
}