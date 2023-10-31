using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Request
{
    public record EvaluacionAddDtoRequest(int TituloId, int Calificacion, string Resenia, string Email, string NombreUsuario);
}
