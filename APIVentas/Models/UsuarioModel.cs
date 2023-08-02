using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIVentas.Models
{
    public class UsuarioModel
    {
        //public UsuarioModel()
        //{
        //    idusuario = 0;
        //}
        public int idusuario { get; set; }
        public string usuario { get; set; }
        public string nombrecompleto { get; set; }      
        public string access_token { get; set; }                
    }
}