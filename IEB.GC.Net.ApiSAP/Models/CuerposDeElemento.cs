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

    public partial class CuerposDeElemento
    {
        public int Id_Cuerpo { get; set; }
        [Display(Name = "Tipo de elemento (mm)")]
        public string TipoDeElemento { get; set; }
        [Display(Name = "Ancho inferior (mm)")]
        public double Ancho_inferior { get; set; }
        [Display(Name = "Largo inferior (mm)")]
        public double Largo_Inferior { get; set; }
        [Display(Name = "Ancho superior (mm)")]
        public double Ancho_superior { get; set; }
        [Display(Name = "Largo superior (mm)")]
        public double Largo_Superior { get; set; }
        [Display(Name = "Altura (mm)")]
        public double Altura { get; set; }
        [Display(Name = "Nivel de Tension")]
        public string Nivel_de_tension { get; set; }

        public Nullable<int> Id_Elemento { get; set; }
        [Display(Name = "Tipo de diagona")]
        public string Tipo_de_diagonal { get; set; }
        [Display(Name = "Divisiones adicionales")]
        public int Division_adicional { get; set; }
        [Display(Name = "Perfil de montante")]
        public string PerfilMontante { get; set; }
        [Display(Name = "Perfil de diagonal")]
        public string PerfilDiagonarl { get; set; }
        [Display(Name = "Perfil de montante de viga")]
        public string PerfilMontanteViga { get; set; }
        [Display(Name = "Perfil de diagonal de viga")]
        public string PerfilDiagonalViga { get; set; }
        [Display(Name = "Perfil de cierre")]
        public string PerfilCierre { get; set; }
        [Display(Name = "Perfil de montante de castillete")]
        public string PerfilMontanteCastillete { get; set; }
        [Display(Name = "Perfil de diagonal de castillete")]
        public string PerfilDiagonalCastillete { get; set; }
    
        public virtual Elemento Elemento { get; set; }
    }
}
