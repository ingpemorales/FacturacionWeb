using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace FacturaMVC.Models
{
    public class Articulo
    {
        [Key]
        public int IdArticulo { get; set; }

        [ Required]
        public string Descripcion { get; set; }

        [Column(TypeName = "decimal(18, 2)"), Required]
        public decimal Costo { get; set; }

        [Column(TypeName = "decimal(18, 2)"), Required]
        public decimal Precio { get; set; }

        public Boolean Activo { get; set; }
    }
}
