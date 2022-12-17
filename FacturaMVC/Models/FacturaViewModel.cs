using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace FacturaMVC.Models
{
    public class FacturaViewModel

		
    {
        public Factura factura { get; set; }
        public List<FacturaDetalle> facturaDetalle { get; set; }
    }
}
