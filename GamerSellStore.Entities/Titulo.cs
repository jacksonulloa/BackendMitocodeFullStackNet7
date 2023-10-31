using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Entities
{
    public class Titulo : EntidadBase
    {
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public Publisher Publisher { get; set; } = new Publisher();
        public int PublisherId { get; set; } = 0;
        public Genero Genero { get; set; } = new Genero();
        public int GeneroId { get; set; } = 0;
        public Consola Consola { get; set; } = new Consola();
        public int ConsolaId { get; set; } = 0;
        public Clasificacion Clasificacion { get; set; } = new Clasificacion();
        public int ClasificacionId { get; set; } = 0;
        public double Costo { get; set; } = 0;
        public string Moneda { get; set; } = "";
        public int Stock { get; set; } = 0;
        public int Agotado { get; set; } = 0;
        public string? ImageUrl { get; set; } = default;        
        public int AnioPublicacion { get; set; } = DateTime.Now.Year;
    }
}
