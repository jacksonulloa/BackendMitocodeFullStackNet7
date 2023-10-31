using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Entities
{
    public class Evaluacion : EntidadBase
    {
        public DateTime Fecha { get; set; } = DateTime.Now;
        public Titulo Titulo { get; set; } = new Titulo();
        public int TituloId { get; set; } = 0;
        public Cliente Cliente { get; set; } = new Cliente();
        public int ClienteId { get; set; } = 0;
        public int Calificacion { get; set; } = 0;
        public string Resenia { get; set; } = "";
    }
}
