using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebVentas.Helpers;
using WebVentas.Models;
using System.Web.Script.Serialization;
using WebVentas.Seguridad;


namespace WebVentas.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        
        public async Task<ActionResult> Index()
        {
            string Usuario;
            string Contrasena;
            Usuario = "Gerente";
            Contrasena = "password";
            UsuarioModel response = new UsuarioModel();
            response = await obtenerCredenciales(Usuario, Contrasena);

            if (response.idusuario == 0)
            {
                SessionPersister.Usuario = null;
                SessionPersister.IdUsuario = null;
                return RedirectToAction("Index", "Home");
            }
            else
            {

                SessionPersister.Usuario = response.usuario;                                             
                SessionPersister.AccessToken = response.access_token.ToString();
                SessionPersister.IdUsuario = response.idusuario.ToString();
                Session["Usuario"] = SessionPersister.Usuario;
                Session["NomUsuario"] = response.nombrecompleto;
            
                return RedirectToAction("Dashboard", "Dashboard");
            }
        }
        public async Task<UsuarioModel> obtenerCredenciales(string usuario, string password )
        {

            ApiHelpers apih = new ApiHelpers();
            UsuarioModel response = new UsuarioModel();
            try
            {

                List<parameter> lsParametros = new List<parameter>
                {
                    new parameter{Name="userid",value = usuario.ToString()},
                    new parameter{Name="password",value = password.ToString()}

                };

                string result = apih.CallApiMethod(true, "post", "api/login/login", lsParametros);
                response = new JavaScriptSerializer().Deserialize<UsuarioModel>(result);

            }
            catch (Exception ex)
            {
                //LU.Log20.GrabarError(ex.Message, ex.StackTrace);
            }
            return response;
        }
    }
}