using GamerSellStore.Dtos.Request.Usuario;
using GamerSellStore.Dtos.Response;
using GamerSellStore.Dtos.ResponseBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);

        Task<BaseResponse> RegistrarUsuarioAsync(RegisterDtoRequest request);

        Task<BaseResponse> RequestTokenToResetPasswordAsync(ResetPasswordDtoRequest request);

        Task<BaseResponse> ResetPasswordAsync(ConfirmPasswordDtoRequest request);

        Task<BaseResponse> ChangePasswordAsync(string email, ChangePasswordDtoRequest request);
    }
}
