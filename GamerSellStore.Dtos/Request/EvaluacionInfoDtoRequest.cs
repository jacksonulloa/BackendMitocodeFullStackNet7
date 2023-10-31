using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Request
{
    public class EvaluacionInfoDtoRequest
    {
        public string? Titulo { get; set; } = default;
        public string? Publisher { get; set; } = default;
        public string? Genero { get; set; } = default;
        public string? Consola { get; set; } = default;
    }
}
