using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel;

namespace ConsumeWebApi.Models
{
    
    public class DoctosViewModels
    {
        
        [Key]
        public int DoctoID { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Fecha { get; set; }
        public int ProveedorID { get; set; }
        [DisplayName("Proveedor")]
        public string ProveedorNombre { get; set; }
        [Required]
        [Range(0.01, 100000.00,ErrorMessage = "La cantidad debe estar entre 0.01 y 100000.00")]
        public decimal Monto { get; set; }
        public int MonedaID { get; set; }
        [DisplayName("Moneda")]
        public string MonedaNombre { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Comentario { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }
        public Guid UsuarioID { get; set; }

    }
}