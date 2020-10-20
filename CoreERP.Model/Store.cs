using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class Store
    {
        public int id_deposito { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Depósito debe tener un valor.")]
        public String desposito { get; set; }
        public String descripcion { get; set; }
    }
}
