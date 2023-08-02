using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEVentas
{
    public class ENT_Indicadores
    {
        public ENT_Indicadores()
        {
            Objetivo = 0;
            Acumulado = 0;

        }
        public decimal Objetivo { get; set; }
        public decimal Acumulado { get; set; }
       
    }
}
