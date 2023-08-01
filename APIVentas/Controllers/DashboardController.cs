using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;

namespace APIVentas.Controllers
{
    [TokenAuthentication]
    public class DashboardController : ApiController
    {          
        [Route("Dashboard/ListarDatos")]
        [HttpPost]
        
        public IHttpActionResult ListarDatos(int v_AsesorID)
        {
           
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, v_AsesorID));

       
        }

    }
}