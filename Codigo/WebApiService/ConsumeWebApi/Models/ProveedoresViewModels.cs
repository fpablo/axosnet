using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumeWebApi.Models
{
    public class ProveedoresViewModels
    { 
        [Key]
        public int ProveedorID { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }
    }
}