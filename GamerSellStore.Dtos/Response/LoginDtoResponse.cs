using GamerSellStore.Dtos.ResponseBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Response
{
    public class LoginDtoResponse : BaseResponse
    {
        public string NombreCompleto { get; set; } = "";
        public List<string> Roles { get; set; } = new List<string>();
        public string Token { get; set; } = default!;
    }
}
