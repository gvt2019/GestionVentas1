using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.Http;
using BEVentas;


namespace DALVentas
{public class daDashboard
    {
        public ENT_Indicadores listar_indicadores(int asesorID, string Periodo)
        {
            ENT_Indicadores eIndicadores = new ENT_Indicadores();

            daSQL objDA = new daSQL();
            try
            {
                DataSet dataSet = new DataSet();
                //ComunicacionesCalidadModel dashModel = new ComunicacionesCalidadModel();
                var parametros = new List<Parameter>();

                parametros.Clear();
                parametros.Add(new Parameter { Name = "asesorID", Value = asesorID.ToString() });
                parametros.Add(new Parameter { Name = "periodo", Value = Periodo.ToString() });
                dataSet = objDA.ExecuteDataAdapter("sp_traeIndicadores_asesor", parametros);
                if (dataSet.Tables.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        ENT_Criterios.ENT_Criterio_Asesor oAsesor = new ENT_Criterios.ENT_Criterio_Asesor();
                        eIndicadores.Objetivo = Convert.ToDecimal(dr["Objetivo"].ToString());                        
                    }
                    foreach (DataRow dr in dataSet.Tables[1].Rows)
                    {
                        ENT_Criterios.ENT_Criterio_Asesor oAsesor = new ENT_Criterios.ENT_Criterio_Asesor();
                        eIndicadores.Acumulado = Convert.ToDecimal(dr["Acumulado"].ToString());
                    }
                    
                }
            }
            catch (Exception ex)
            {

            }
            return eIndicadores;
        }
    }
}
