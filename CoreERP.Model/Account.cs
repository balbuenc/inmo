using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class Account
    {
        public int id_cuenta { get; set; }
        public int id_tipo_cuenta { get; set; }
        public int id_banco { get; set; }
        public string deniminacion { get; set; }
        public string nro_cuenta { get; set; }
    }
}
