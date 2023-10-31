using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Entities.Info
{
    public class EvaluacionInfo
    {
        public int Id { get; set; } = 0;
        public string FechaStr { get; set; } = "";
        public string HoraStr { get; set; } = "";
        public string Titulo { get; set; } = "";
        public string Genero { get; set; } = "";
        public string Consola { get; set; } = "";
        public string Publisher { get; set; } = "";
        public string Clasificacion { get; set; } = "";
        public string Cliente { get; set; } = "";
        public double PrecioUnitario { get; set; } = 0;
        public string Moneda { get; set; } = "";
        public int CantidadDisponible { get; set; } = 0;
        public string? ImageUrl { get; set; } = default;
        public int Calificacion { get; set; } = 0;
        public string Resenia { get; set; } = "";
        public string Estado { get; set; } = "";
    }
}
