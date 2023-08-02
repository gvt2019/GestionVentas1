using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BEVentas;
using System.Web.Mvc;

namespace WebVentas.ViewModels { 

    public class DashboardViewModel
    {
        public string droplListaAsesor { get; set; }
        public IEnumerable<SelectListItem> ListaAsesores = new List<SelectListItem>();

        public ENT_Criterios oCriterio { get; set; }
        public ENT_Indicadores oIndicador { get; set; }

        public DashboardViewModel()
        { ENT_Criterios ocriterio = new ENT_Criterios();
          ENT_Indicadores oIndicador = new ENT_Indicadores();  
        }
    }
}