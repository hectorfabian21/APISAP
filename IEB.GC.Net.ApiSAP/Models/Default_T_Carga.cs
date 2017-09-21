using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IEB.GC.Net.ApiSAP.Models
{
    public class Default_T_Carga
    {
        public double Cortante_basal { get; set; }
        public double K_exponente { get; set; }

        public double Velocidad_viento { get; set; }
        public int Tipo_exposicion { get; set; }
        public double Factor_importancia { get; set; }
        public double Factor_topografia { get; set; }
        public double Factor_rafaga { get; set; }
        public double Factor_direccion { get; set; }

    }
}