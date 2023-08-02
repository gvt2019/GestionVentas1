using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebVentas.Helpers;
using WebVentas.Seguridad;
using WebVentas.ViewModels;
using BEVentas;

namespace WebVentas.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            ListarAsesores();
            ObtenerDashboard(2, "20230801");
            return View();
        }
        public JsonResult ListarAsesores()
        {
            try
            {
                ApiHelpers apih = new ApiHelpers();
                List<parameter> lsParametros = new List<parameter>();
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                jsSerializer.MaxJsonLength = 500000000;
                DashboardViewModel oDashboardViewModel = new DashboardViewModel();
                
                string gerenteID = (SessionPersister.IdUsuario != null ? SessionPersister.IdUsuario : "0");

                lsParametros.Add(new parameter { Name = "gerenteID", value = gerenteID.ToString() });
                string result = apih.CallApiMethod(true, "post", "Dashboard/ListarAsesores", lsParametros);

                if (result != "")
                {
                    oDashboardViewModel.oCriterio = jsSerializer.Deserialize<ENT_Criterios>(result);

                    ViewBag.Asesores = new SelectList(oDashboardViewModel.oCriterio.Dash_Criterios_Asesor, "AsesorID", "Nombre");

                }



            }
            catch (Exception)
            {
            }
            return null;
        }
        public JsonResult ObtenerDashboard(int asesorID , string periodo)
        {
            
            try
            {
                ApiHelpers apih = new ApiHelpers();
                List<parameter> lsParametros = new List<parameter>();
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                jsSerializer.MaxJsonLength = 500000000;
                DashboardViewModel oDashboardViewModel = new DashboardViewModel();
                lsParametros.Add(new parameter { Name = "asesorID", value = asesorID.ToString() });
                lsParametros.Add(new parameter { Name = "periodo", value = periodo.ToString() });

                string result = apih.CallApiMethod(true, "post", "Dashboard/ListarIndicadores", lsParametros);


                if (result != "")
                {
                    oDashboardViewModel.oIndicador = jsSerializer.Deserialize<ENT_Indicadores>(result);


                    ViewBag.objetivo = oDashboardViewModel.oIndicador.Objetivo;
                    ViewBag.acumulado = oDashboardViewModel.oIndicador.Acumulado;

                }


            }
            catch (Exception)
            {


            }           
            return null;
            
        }
    }
}