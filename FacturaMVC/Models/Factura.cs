using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace FacturaMVC.Models
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }

        public string Cliente { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal PorcentajeImpuesto { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Impuesto { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }

        public List<FacturaDetalle> FacturaDetalles { get; set; }
    }
}
