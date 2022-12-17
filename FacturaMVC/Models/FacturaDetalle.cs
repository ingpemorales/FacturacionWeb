using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FacturaMVC.Models
{
    public class FacturaDetalle
    {
        [Key]
        public int IdFacturaDetalle { get; set; }        

        public int IdFactura { get; set; }
        
        public int IdArticulo { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecioVenta { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Impuesto { get; set; }
    }
}
