using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEVentas
{
    public class ENT_Criterios
    {
        public List<ENT_Criterio_Asesor> Dash_Criterios_Asesor { get; set; }
        //public List<ENT_Criterio_Periodo> Dash_Criterios_Latencia { get; set; }

        public class ENT_Criterio_Asesor
        {
            public int AsesorID { get; set; }
            public string Nombre { get; set; }
        }
        public ENT_Criterios()
        {
            // Inicialización en el constructor
            Dash_Criterios_Asesor = new List<ENT_Criterio_Asesor>();
        }

    }

}