using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Response
{
    public class TituloSearchDtoResponse
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Publisher { get; set; } = "";
        public string Genero { get; set; } = "";
        public string Consola { get; set; } = "";
        public double Costo { get; set; } = 0;
        public string Moneda { get; set; } = "";
        public int Stock { get; set; } = 0;
        public string AgotadoDesc { get; set; } = "";
        public string? ImageUrl { get; set; } = default;
        public string Clasificacion { get; set; } = "";
        public int AnioPublicacion { get; set; } = DateTime.Now.Year;
        public string EstadoDesc { get; set; } = "";
    }
}
