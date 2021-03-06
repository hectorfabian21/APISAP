//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IEB.GC.Net.ApiSAP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Cargas_Automatica
    {
        public int Id { get; set; }
        public int Id_subestacion { get; set; }
        public int id_caso { get; set; }
        [Display(Name = "Cortante basal")]
        public double Cortante_basal { get; set; }
        [Display(Name = "Coeficiente K")]
        public double K_exponente { get; set; }
        [Display(Name = "Velocidad del viento")]
        public double Velocidad_viento { get; set; }
        [Display(Name = "Tipo de exposicion")]
        public string Tipo_exposicion { get; set; }
        [Display(Name = "Factor de importancia")]
        public double Factor_importancia { get; set; }
        [Display(Name = "Factor de topografia")]
        public double Factor_topografia { get; set; }
        [Display(Name = "Factor de Rafaga")]
        public double Factor_rafaga { get; set; }
        [Display(Name = "Factor de direccion")]
        public double Factor_direccion { get; set; }
        [Display(Name = "R de porticos")]
        public double R_Porticos { get; set; }
        [Display(Name = "R de equipo")]
        public double R_Equipos { get; set; }
    
        public virtual Caso Caso { get; set; }
        public virtual SubestacionSAP SubestacionSAP { get; set; }
    }
}
