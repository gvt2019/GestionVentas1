using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIVentas.Utilitarios
{
    public class Constantes
    {
        public static class APIResponse
        {
            public static class Message
            {
                public const string Error = "Ocurrio un inconveniente al ejecutar procedimiento";
            }

            public static class LoginMessage
            {
                public const string Credenciales = "No se ingresaron las credenciales";

                public const string ContrasenaVacia = "No se ha ingresado una contraseña.";

                public const string IdentificacionCuenta = "Ocurrió un problema con la identificación de la cuenta.";

                public const string IdentificacionURL = "Ocurrió un problema con la identificación de la URL. Vuelva a ingresar a la URL de Cambio de Clave.";
            }

            public const string Ok = "1";

            public const string Error = "0";

            public const string Separador = "|";

            public const string ErrorHttp = "Fallo la llamada al servicio";

            public const string ErrorCath = "Error: {0}";
        }
    }
    public class Util
    {
    }
}