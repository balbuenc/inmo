using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class Purchase
    {
        public int id_compra { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Factura debe tener un valor.")]
        public String factura { get; set; }
        public int id_proveedor { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Importe debe tener un valor.")]
        public decimal importe { get; set; }
        public int id_moneda { get; set; }
    }
}
