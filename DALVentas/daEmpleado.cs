using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net.Http;
using BEVentas;

namespace DALVentas
{
    public class daEmpleado
    {
        public ENT_Criterios Listar_Asesores(int gerenteID)
        {
            ENT_Criterios eCriterio = new ENT_Criterios();     
           
            daSQL objDA = new daSQL();            
            try
            {
                DataSet dataSet = new DataSet();
                //ComunicacionesCalidadModel dashModel = new ComunicacionesCalidadModel();
                var parametros = new List<Parameter>();

                parametros.Clear();
                parametros.Add(new Parameter { Name = "gerenteID", Value = gerenteID.ToString() });
                dataSet = objDA.ExecuteDataAdapter("sp_listar_Asesores", parametros);
                if (dataSet.Tables.Count > 0)
                {
                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        ENT_Criterios.ENT_Criterio_Asesor oAsesor = new ENT_Criterios.ENT_Criterio_Asesor();
                        oAsesor.AsesorID = Convert.ToInt16(dr["EmpleadoID"].ToString());
                        oAsesor.Nombre = dr["Nombre"].ToString();
                        eCriterio.Dash_Criterios_Asesor.Add(oAsesor);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return eCriterio;
        }
    }
}
