using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class Budget
    {
        public int id_presupuesto { get; set; }
        public int id_cliente { get; set; }
        public int id_funcionario { get; set; }
        public DateTime fecha { get; set; }
        public String estado { get; set; }
    }
}
