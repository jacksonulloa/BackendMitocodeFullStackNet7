using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Response
{
    public class EvaluacionDtoResponse
    {
        public int EvaluacionId { get; set; } = 0;
        public string FechaStr { get; set; } = "";
        public string HoraStr { get; set; } = "";
        public string Titulo { get; set; } = "";
        public string Cliente { get; set; } = "";
        public int Calificacion { get; set; } = 0;
        public string Resenia { get; set; } = "";
    }
}
