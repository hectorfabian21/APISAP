using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IEB.GC.Net.ApiSAP.Models
{
    public class Cuerpo
    {
        [Key]
        [Required]
        public int id_Cuerpo { get; set; }
        [Required]
        public string Tipo_cuerpo { get; set; }        
        [Required]
        [Range(0,6000,ErrorMessage ="Valor por fuera de los rangos definidos (0 - 6000)")]
        public double ancho_in { get; set; }
        [Required]
        [Range(0, 6000, ErrorMessage = "Valor por fuera de los rangos definidos (0 - 6000)")]
        public double largo_in { get; set; }
        [Required]
        [Range(0, 6000, ErrorMessage = "Valor por fuera de los rangos definidos (0 - 6000)")]
        public double ancho_su { get; set; }
        [Required]
        [Range(0, 6000, ErrorMessage = "Valor por fuera de los rangos definidos (0 - 6000)")]
        public double largo_su { get; set; }
        [Required]
        [Range(0, 50000, ErrorMessage = "Valor por fuera de los rangos definidos (0 - 50000)")]
        public double altura { get; set; }       
   
    }
}