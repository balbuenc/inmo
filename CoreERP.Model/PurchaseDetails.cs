using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class PurchaseDetails
    {
        public int id_compra { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Cantidad debe tener un valor.")]
        public decimal cantidad { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Monto debe tener un valor.")]
        public decimal monto { get; set; }

        public String descripcion { get; set; }

    }
}
