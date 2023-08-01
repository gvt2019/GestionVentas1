using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using  System.Security.Claims;

namespace APIVentas.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(string userId, string password)
        {
            // Aquí implementa la lógica de autenticación.
            // Si las credenciales son válidas, puedes generar un token JWT.

            bool isAdmin = true; // Determina si el usuario es un administrador o no.

            // Genera el token JWT.
            string token = TokenHandler.GenerateToken(userId, "NombreUsuario", isAdmin);

            return Ok(new { token });
        }

        [HttpGet]
        [Route("resource")]
        public IHttpActionResult GetProtectedResource()
        {
            // Aquí puedes verificar el token recibido en el encabezado de autorización.
            // Por ejemplo:
            string token = Request.Headers.Authorization.Parameter;

            ClaimsPrincipal principal = TokenHandler.ValidateToken(token);

            if (principal == null)
            {
                return Unauthorized();
            }

            // Si el token es válido, continúa con la lógica para obtener el recurso protegido.

            return Ok("Recurso protegido obtenido.");
        }
    }
}
