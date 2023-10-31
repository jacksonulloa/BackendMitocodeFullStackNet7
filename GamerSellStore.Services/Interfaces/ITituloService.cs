using GamerSellStore.Dtos.Request;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Dtos.ResponseBase;

namespace GamerSellStore.Services.Interfaces
{
    public interface ITituloService
    {
        Task<BaseResponseLista<TituloSearchDtoResponse>> ListarAsync(TituloBusqueda busqueda,
            CancellationToken cancellationToken = default);
        Task<BaseResponse> ActualizarAsync(TituloDtoRequest request);
        Task<BaseResponse> AddAsync(TituloAddDtoRequest request);
        Task<BaseResponseSingular<TituloFindDtoResponse>> BuscarPorIdAsync(int id);
        Task<BaseResponse> EliminarAsync(int id);
        Task<BaseResponseLista<TituloSearchDtoResponse>> ListarAsync();
        Task<BaseResponseLista<TituloSearchDtoResponse>> ListarAsync(TituloSearchDtoRequest request);
    }
}