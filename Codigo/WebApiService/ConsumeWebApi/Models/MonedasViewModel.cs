using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsumeWebApi.Models
{
    public class MonedasViewModel
    {
        [Key]
        public int MonedaID { get; set; }

        [Required]
        [StringLength(30,ErrorMessage ="El campo {0} debe estar entre {2} y {1} caracteres",MinimumLength =3 )]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Texto Importe en Letra")]
        public string TextoImpteLetra { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres", MinimumLength = 1)]
        [Display(Name = "Símbolo")]
        public string Simbolo { get; set; }

        public DateTime? FechaHoraCreacion { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }

}
}