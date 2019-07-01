using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiService.Models
{
    public class DoctosModels
    {
        [Key]
        public int DoctoID { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal Monto { get; set; }
        public int ProveedorID { get; set; }
        public string ProveedorNombre { get; set; }
        public int MonedaID { get; set; }
        public string MonedaNombre { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }
        public Guid UsuarioID { get; set; }
    }
}