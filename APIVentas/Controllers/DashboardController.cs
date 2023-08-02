using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using DALVentas;
using APIVentas.Utilitarios;
using BEVentas;

namespace APIVentas.Controllers
{
    [TokenAuthentication]
    public class DashboardController : ApiController
    {          
        [Route("Dashboard/ListarAsesores")]
        [HttpPost]
        
        public IHttpActionResult ListarDatos(int gerenteID)
        {
            daEmpleado oEmpleado= new daEmpleado();
           ENT_Criterios oCriterios = new ENT_Criterios();
            
            oCriterios = oEmpleado.Listar_Asesores(gerenteID);
            
            if (oCriterios == null)
            {
                //oCriterios = Constantes.APIResponse.Error + Constantes.APIResponse.Separador + Constantes.APIResponse.Message.Error;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, oCriterios));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, oCriterios));
                   
        }

        [Route("Dashboard/ListarIndicadores")]
        [HttpPost]

        public IHttpActionResult ListarIndicadores(int asesorID,string periodo)
        {
            daDashboard oDashboard = new daDashboard();
            ENT_Indicadores oIndicador = new ENT_Indicadores();

            oIndicador = oDashboard.listar_indicadores(asesorID,periodo);

            if (oIndicador == null)
            {
                //oCriterios = Constantes.APIResponse.Error + Constantes.APIResponse.Separador + Constantes.APIResponse.Message.Error;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, oIndicador));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, oIndicador));

        }

    }
}