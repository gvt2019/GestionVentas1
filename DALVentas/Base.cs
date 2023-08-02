using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Xml;

namespace DALVentas
{
    public class Base 
    {
        //public string connectionString;
        protected string strCadenaConexionBaseSQL = ConfigurationManager.ConnectionStrings["accesoSQL"].ToString();
        public string ObtenerCadenaConexion()
        {
            string cadenaConexion = "";            
            try
            {
                cadenaConexion = strCadenaConexionBaseSQL;


            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                
            }

            return cadenaConexion;
        }
    }
}
