using GamerSellStore.Dtos.Request;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Dtos.ResponseBase;

namespace GamerSellStore.Services.Interfaces
{
    public interface IClasificacionService
    {
        Task<BaseResponse> ActualizarAsync(ClasificacionDtoRequest request);
        Task<BaseResponse> AddAsync(ClasificacionAddDtoRequest request);
        Task<BaseResponseSingular<ClasificacionSearchDtoResponse>> BuscarPorIdAsync(int id);
        Task<BaseResponse> EliminarAsync(int id);
        Task<BaseResponseLista<ClasificacionSearchDtoResponse>> ListarAsync();
        Task<BaseResponseLista<ClasificacionSearchDtoResponse>> ListarAsync(string nombre);
    }
}