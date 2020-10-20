using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class Stock
    {
        public int id_stock { get; set; }
        public int id_deposito { get; set; }
        public int id_producto { get; set; }
        public decimal cantidad { get; set; }
        public int id_proveedor { get; set; }
    }
}
