using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSellStore.Entities
{
    public static class Utilitario
    {
        public static int ObtenerTotalPaginas(int total, int filas)
        {
            var totalPaginas = (total == 0) ? 0 : ((total % filas == 0) ? (total / filas) : ((total / filas) + 1));
            return totalPaginas;
        }

        public static string GenerarOperacion(DateTime fechahoraactual)
        {
            string resp = $"{fechahoraactual.Year.ToString()}{fechahoraactual.Month.ToString().PadLeft(2, '0')}{fechahoraactual.Day.ToString().PadLeft(2, '0')}" +
                $"{fechahoraactual.Hour.ToString().PadLeft(2, '0')}{fechahoraactual.Minute.ToString().PadLeft(2, '0')}{fechahoraactual.Second.ToString().PadLeft(2, '0')}";
            return resp;
        }
    }
}
