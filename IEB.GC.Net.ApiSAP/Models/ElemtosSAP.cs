using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAP2000v19;
namespace IEB.GC.Net.ApiSAP.Models
{
    public class ElemtosSAP
    {        
        public cOAPI Objeto { get; set; }
        public cSapModel Modelo { get; set; }
        public bool estadoguardar { get; set; }
        public bool estadocorrerproyecto { get; set; }
        public bool estadoreaccionesservicio { get; set; }
        public bool estadoreaccionesultimas { get; set; }
        public bool estadoreaccionespernos { get; set; }
        public bool estadocorrerdiseño { get; set; }
        public bool estadoresultadosdiseño { get; set; }

        public bool estadosalir { get; set; }


        public ElemtosSAP()
        {

            estadocorrerdiseño = false;
            estadocorrerproyecto = false;
            estadoguardar = false;
            estadoreaccionespernos = false;
            estadoreaccionesservicio = false;
            estadoreaccionesultimas = false;
            estadoresultadosdiseño = false;
            estadosalir = false;            
        }

    }
}