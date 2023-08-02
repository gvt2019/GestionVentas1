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
        public JsonResult ObtenerDashboard(string strPeriodo = "", string strMesesHistorico = "", string strTipo = "DUR", string strCodint = "", string strCodempleado = "", string strDifMinutos = "0", string presExterno = "-1")
        {
            //ComunicacionesCalidadModel ojbComunicaciones = new ComunicacionesCalidadModel();
            try
            {
                ApiHelpers apih = new ApiHelpers();
                List<parameter> lsParametros = new List<parameter>();
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                jsSerializer.MaxJsonLength = 500000000;

                //string idcliente = (SessionPersister.IdCliente != null ? SessionPersister.IdCliente : "0");
                //string idusuario = SessionPersister.IdUsuario;

                //lsParametros.Add(new parameter { Name = "v_IdCliente", value = idcliente.ToString() });
                //lsParametros.Add(new parameter { Name = "strIdUsuario", value = idusuario.ToString() });
                //lsParametros.Add(new parameter { Name = "strPeriodo", value = strPeriodo.ToString() });
                //lsParametros.Add(new parameter { Name = "strMeses", value = strMesesHistorico.ToString() });
                //lsParametros.Add(new parameter { Name = "strTipo", value = strTipo.ToString() });
                //lsParametros.Add(new parameter { Name = "strCodint", value = strCodint.ToString() });
                //lsParametros.Add(new parameter { Name = "strCodempleado", value = strCodempleado.ToString() });
                //lsParametros.Add(new parameter { Name = "strDifMinutos", value = strDifMinutos.ToString() });
                //lsParametros.Add(new parameter { Name = "prEsExterno", value = presExterno.ToString() });

                //string v_IdCliente, string strFechaInicio, string strFechaFin, string strCodint, string strCodempleado

                string result = apih.CallApiMethod(true, "post", "Dashboard/ListarDashComunicacionesCalidad", lsParametros);


                if (result != "")
                {
                    //ojbComunicaciones = jsSerializer.Deserialize<ComunicacionesCalidadModel>(result);
                }



            }
            catch (Exception)
            {


            }
            //return Json(ojbComunicaciones);

            return null;
            //return Json(modelDashBoard);
        }
    }
}