using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IEB.GC.Net.ApiSAP.Models
{
    public class Subestacioncaso
    {
        public int Id { get; set; }
        public string nombre_caso { get; set; }
        public string descripcion_caso { get; set; }
        public int id_subestacion { get; set; }
        
        public string Revisor { get; set; }
        public string Aprobador { get; set; }
        public string NombreProyecto { get; set; }
        public string NombreSubestacion { get; set; }
        public string NombreModelo { get; set; }
       
    }
}