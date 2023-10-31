using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Request.Usuario
{
    public record LoginDtoRequest(string Username, string Password);
}
