using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Request
{
    public class TituloDtoRequest
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public int PublisherId { get; set; } = 0;
        public int GeneroId { get; set; } = 0;
        public int ConsolaId { get; set; } = 0;
        public double Costo { get; set; } = 0;
        public int Stock { get; set; } = 0;
        public int Agotado { get; set; } = 0;
        public string? ImageUrl { get; set; } = default;
        public int ClasificacionId { get; set; } = 0;
        public int AnioPublicacion { get; set; } = DateTime.Now.Year;
        public int Estado { get; set; } = 0;
        public string? Base64Image { get; set; }
        public string? NombreArchivo { get; set; }
    }
}
