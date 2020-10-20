using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class BudgetDetails
    {
        public int id_presupuesto { get; set; }
        public int id_producto { get; set; }
        public int id_descuento { get; set; }
        public decimal cantidad { get; set; }
        public decimal costo { get; set; }
        public decimal precio { get; set; }
    }
}
