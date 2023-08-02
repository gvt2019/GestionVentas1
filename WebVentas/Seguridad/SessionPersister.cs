using System;
using System.Collections.Generic;

using System.Web;

namespace WebVentas.Seguridad
{
    public static class SessionPersister
    {
        static string usernameSessionvar = "Usuario";
        static string passwordSessionvar = "Contrasena";
        static string idUserSessionvar = "IdUsuario";
        static string accessTokenSessionvar = "AccessToken";
        public static string Usuario
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session[usernameSessionvar];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }

            set
            {
                HttpContext.Current.Session[usernameSessionvar] = value;
            }
        }

        public static string Contrasena
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session[passwordSessionvar];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }

            set
            {
                HttpContext.Current.Session[passwordSessionvar] = value;
            }
        }
        public static string AccessToken
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session[accessTokenSessionvar];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }

            set
            {
                HttpContext.Current.Session[accessTokenSessionvar] = value;
            }
        }
        public static string IdUsuario
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session[idUserSessionvar];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }

            set
            {
                HttpContext.Current.Session[idUserSessionvar] = value;
            }
        }

    }
}