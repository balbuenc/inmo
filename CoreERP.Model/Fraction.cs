using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class Fraction
    {
        public int id { get; set; }
        public int fraccion { get; set; }
        public decimal factor_descuento { get; set; }

        public string boca { get; set; }

        public decimal limite_descuento { get; set; }

        public decimal minimo_descuento { get; set; }

        public int id_boca_cobranza { get; set; }

        public string positivo { get; set; }

    }
}
