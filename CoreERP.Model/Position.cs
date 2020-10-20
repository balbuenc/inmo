using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreERP.Model
{
    public class Position
    {
        public int id_cargo { get; set; }
        public int id_area { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Cargo debe tener un valor.")]
        public String cargo { get; set; }
    }
}
