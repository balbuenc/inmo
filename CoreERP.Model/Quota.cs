using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class Quota
    {
        public int id_credito { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Cuota debe tener un valor.")]
        public int cuota { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Monto Capital debe tener un valor.")]
        public decimal monto_capital { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Monto Interes debe tener un valor.")]
        public decimal monto_interes { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Vencimiento debe tener un valor.")]
        public DateTime vencimiento { get; set; }
        public DateTime fecha_pago { get; set; }
        public decimal multa { get; set; }
        public decimal mora { get; set; }
    }
}
