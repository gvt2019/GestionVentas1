//using General.Librerias.Utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DALVentas
{         
        public class daSQL : Base
        {
            private string connectionString;

            public daSQL(int IdCliente = 0)
            {
                try
                {
                    //connectionString = ConfigurationManager.ConnectionStrings["accesoSQL"].ConnectionString;
                    //if (cifrado)
                    //{
                    //    connectionString = Seguridad.descifrarAESHex(connectionString);
                    //} string strCadenaConexion = "";

                    connectionString = ObtenerCadenaConexion();

                }
                catch (Exception ex)
                {
                    connectionString = "";
                    //Log20.GrabarError(ex.Message, ex.StackTrace);
                }
            }
 

            public string ExecuteComand(string store, List<Parameter> parameters)
            {
                string rpta = "";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(store, con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            for (int i = 0; i < parameters.Count; i++)
                            {
                                if (!String.IsNullOrEmpty(parameters[i].Name))
                                {
                                    cmd.Parameters.AddWithValue(parameters[i].Name, parameters[i].Value);
                                }
                            }
                        }
                        object data = cmd.ExecuteScalar();
                        if (data != null) rpta = data.ToString();
                    }
                    catch (Exception ex)
                    {
                        //Log20.GrabarError(ex.Message, ex.StackTrace);
                    }
                }
                return rpta;
            }
            public async Task<string> ExecuteComandAsync(string store, List<Parameter> parameters)
            {
                string rpta = "";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(store, con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            for (int i = 0; i < parameters.Count; i++)
                            {
                                if (!String.IsNullOrEmpty(parameters[i].Name))
                                {
                                    cmd.Parameters.AddWithValue(parameters[i].Name, parameters[i].Value);
                                }
                            }
                        }
                        object data = await cmd.ExecuteScalarAsync();
                        if (data != null) rpta = data.ToString();
                    }
                    catch (Exception ex)
                    {
                        //Log20.GrabarError(ex.Message, ex.StackTrace);
                    }
                }
                return rpta;
            }
            public DataSet ExecuteDataAdapter(string store, List<Parameter> parameters)
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(store, con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            for (int i = 0; i < parameters.Count; i++)
                            {
                                if (!String.IsNullOrEmpty(parameters[i].Name))
                                {
                                    cmd.Parameters.AddWithValue(parameters[i].Name, parameters[i].Value);
                                }
                            }
                        }
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                    }
                    catch (Exception ex)
                    {
                        //Log20.GrabarError(ex.Message, ex.StackTrace);
                    }
                }
                return ds;
            }
            public async Task<DataSet> ExecuteDataAdapterAsync(string store, List<Parameter> parameters)
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(store, con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            for (int i = 0; i < parameters.Count; i++)
                            {
                                if (!String.IsNullOrEmpty(parameters[i].Name))
                                {
                                    cmd.Parameters.AddWithValue(parameters[i].Name, parameters[i].Value);
                                }
                            }
                        }
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        await Task.Run(() => da.Fill(ds));
                    }
                    catch (Exception ex)
                    {
                        //Log20.GrabarError(ex.Message, ex.StackTrace);
                    }
                }
                return ds;
            }

        }

        public class Parameter
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    
}
