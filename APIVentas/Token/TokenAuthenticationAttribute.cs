using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace APIVentas
{ 

    public class TokenAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private static readonly string secretKey = "CJd1VPdWrSTsCg+XUIRQJQMPfpF9/FmmAA=="; // Reemplaza con tu clave secreta

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var tokenHandler = new  JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var token = GetTokenFromRequest(actionContext.Request);

            if (token == null)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                return;
            }

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                SetPrincipal(actionContext, principal);
            }
            catch
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }

        private string GetTokenFromRequest(HttpRequestMessage request)
        {
            if (request.Headers.Authorization != null && request.Headers.Authorization.Scheme == "Bearer")
            {
                return request.Headers.Authorization.Parameter;
            }

            return null;
        }

        private void SetPrincipal(HttpActionContext actionContext, ClaimsPrincipal principal)
        {
            actionContext.RequestContext.Principal = principal;
        }
    }

}