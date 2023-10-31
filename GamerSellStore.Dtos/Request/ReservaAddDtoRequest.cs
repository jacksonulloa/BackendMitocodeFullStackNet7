using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Request
{
    public record ReservaAddDtoRequest(int TituloId, short CantidadReserva, string Email, string NombreUsuario);
}
