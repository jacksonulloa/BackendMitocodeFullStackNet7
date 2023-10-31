using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Response
{
    public class TituloFindDtoResponse
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public int PublisherId { get; set; } = 0;
        //public string Publisher { get; set; } = "";
        public int GeneroId { get; set; } = 0;
        //public string Genero { get; set; } = "";
        public int ConsolaId { get; set; } = 0;
        //public string Consola { get; set; } = "";
        public double Costo { get; set; } = 0;
        public String Moneda { get; set; } = "";
        public int Stock { get; set; } = 0;
        public int Agotado { get; set; } = 0;
        //public string AgotadoDesc { get; set; } = "";
        public string? ImageUrl { get; set; } = default;
        public int ClasificacionId { get; set; } = 0;
        //public string Clasificacion { get; set; } = "";
        public int AnioPublicacion { get; set; } = DateTime.Now.Year;
        public int Estado { get; set; } = 0;
        //public string EstadoDesc { get; set; } = "";        
    }
}
