using WebVentas.Controllers;
//using WebVentas.Models;
using WebVentas.Seguridad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Script.Serialization;

namespace WebVentas.Helpers
{
        public class ApiHelpers
        {

            private string UrlApi = "";
            public string Token { get; set; }

            public ApiHelpers()
            {
               {
                    UrlApi = ConfigurationManager.AppSettings["RutaWebApi"].ToString();
                }


            }
            public string CallApiMethod(bool usatoken, string tipoLlamada, string MetodoApi, List<parameter> listaParametros, StringContent contenBody = null)
            {
                var result = "";
                errorResponse Error = new errorResponse();

                if (UrlApi.LastIndexOf("/") == -1)
                {
                    UrlApi = UrlApi + "/";
                }


                Uri BaseAddress = new Uri(UrlApi);

                string values = "";

                if (listaParametros != null && listaParametros.Count > 0)
                {
                    for (int i = 0; i < listaParametros.Count; i++)
                    {

                        if (i == 0)
                        {
                            values = "?" + listaParametros[i].Name + "=" + listaParametros[i].value;
                        }
                        else
                        {
                            values += "&" + listaParametros[i].Name + "=" + listaParametros[i].value;
                        }
                    }
                }              

                using (var client = new HttpClient())
                {
                    try
                    {
                        if (usatoken)
                        {
                            if (SessionPersister.AccessToken != null)
                            {
                                string token = HttpContext.Current.Session["AccessToken"].ToString();
                                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                            }

                        }
                        client.Timeout = TimeSpan.FromSeconds(600);



                        if (tipoLlamada.ToUpper().ToString() == "GET")
                        {
                            var responseTask = client.GetAsync(String.Format("{0}", BaseAddress + MetodoApi + values));
                            responseTask.Wait();
                            var result_ = responseTask.Result;
                            if (result_.IsSuccessStatusCode)
                            {
                                var readTask = result_.Content.ReadAsStringAsync();
                                readTask.Wait();
                                result = readTask.Result;
                            }
                            else if ((int)result_.StatusCode == 401)
                            {
                                //no autorizado
                                //vuelve a generar token de autentificacion
                                try
                                {
                                    AccountController account = new AccountController();
                                    var usuarioTask = account.obtenerCredenciales(SessionPersister.Usuario, SessionPersister.Contrasena);
                                    usuarioTask.Wait();
                                    var usuario = usuarioTask.Result;
                                    if (usuario.idusuario != 0)
                                    {
                                        SessionPersister.AccessToken = usuario.access_token.ToString();
                                    }
                                    else
                                    {
                                        Error.codigo = 401;
                                        HttpContext.Current.Session["errorResponse"] = null;
                                        HttpContext.Current.Session["errorResponse"] = Error;
                                        result = "Error 401 Acceso no autorizado, se requiere de un token vigente.";
                                        return "";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Error.codigo = 401;
                                    HttpContext.Current.Session["errorResponse"] = null;
                                    HttpContext.Current.Session["errorResponse"] = Error;
                                    result = "Error 401 Acceso no autorizado, se requiere de un token vigente. " + ex.Message.ToString();
                                    return "";
                                }

                            }
                            else if ((int)result_.StatusCode == 500)
                            {
                                //error interno
                                result = "Error 500 se encontraron errores internos";

                                throw new Exception(result);

                            }
                            else
                            {
                                result = "Se encontraron errores comuníquese con su administrador";
                                throw new Exception(result);

                            }
                        }
                        else if (tipoLlamada.ToUpper().ToString() == "POST")
                        {


                            var responseTask = client.PostAsync(String.Format("{0}", BaseAddress + MetodoApi + values), contenBody);
                            responseTask.Wait();
                            var result_ = responseTask.Result;
                            if (result_.IsSuccessStatusCode)
                            {
                                var readTask = result_.Content.ReadAsStringAsync();
                                readTask.Wait();
                                result = readTask.Result;
                            }
                            else if ((int)result_.StatusCode == 401)
                            {
                                //no autorizado
                                //vuelve a generar token de autentificacion
                                try
                                {
                                    AccountController account = new AccountController();
                                    var usuarioTask = account.obtenerCredenciales(SessionPersister.Usuario, SessionPersister.Contrasena );
                                    usuarioTask.Wait();
                                    var usuario = usuarioTask.Result;
                                    if (usuario.idusuario != 0)
                                    {
                                        SessionPersister.AccessToken = usuario.access_token.ToString();
                                    }
                                    else
                                    {
                                        Error.codigo = 401;
                                        HttpContext.Current.Session["errorResponse"] = null;
                                        HttpContext.Current.Session["errorResponse"] = Error;
                                        result = "Error 401 Acceso no autorizado, se requiere de un token vigente.";
                                        return "";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Error.codigo = 401;
                                    HttpContext.Current.Session["errorResponse"] = null;
                                    HttpContext.Current.Session["errorResponse"] = Error;
                                    result = "Error 401 Acceso no autorizado, se requiere de un token vigente. " + ex.Message.ToString();
                                    return "";
                                }

                            }
                            else if ((int)result_.StatusCode == 500)
                            {
                                //error interno
                                var readTask = result_.Content.ReadAsStringAsync();
                                readTask.Wait();
                                result = readTask.Result;


                                //Error = Newtonsoft.Json.JsonConvert.DeserializeObject<errorResponse>(result);
                                Error.codigo = 500;
                                HttpContext.Current.Session["errorResponse"] = null;
                                HttpContext.Current.Session["errorResponse"] = Error;

                                result = "Error 500 Se encontraron errores comuníquese con su administrador";
                                //throw new Exception(result);
                                return "";

                            }
                            else
                            {
                                result = "Se encontraron errores comuníquese con su administrador";
                                //throw new Exception(result);
                                return "";

                            }
                        }



                    }
                    catch (AggregateException ex)
                    {
                        if (result == "")
                        {
                            result = "No se encontro ruta de servicio web";
                        }

                        return "";
                        //throw new Exception(result);
                    }
                    catch (Exception ex)
                    {
                        if (result == "")
                        {
                            result = ex.ToString();
                        }

                        return "";
                        //throw new Exception(result);
                    }

                    return result;
                }
            }
        }

        public class parameter
        {
            public string Name { get; set; }
            public string value { get; set; }
        }

        public class errorResponse
        {
            public int codigo { get; set; }
            public string message { get; set; }
            public string exceptionMessage { get; set; }
            public string exceptionType { get; set; }
            public string stackTrace { get; set; }


        }
    }
