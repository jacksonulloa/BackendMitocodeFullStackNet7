using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Dtos.Response
{
    public class ReservaDtoResponse
    {
        public int ReservaId { get; set; } = 0;
        public string NroTxn { get; set; } = "";
        public string FechaStrTxn { get; set; } = "";
        public string HoraStrTxn { get; set; } = "";
        public string Titulo { get; set; } = "";
        public string Cliente { get; set; } = "";
        public int CantidadReservada { get; set; } = 0;
        public double ImporteTotal { get; set; } = 0;
        public string Moneda { get; set; } = "";
    }
}
