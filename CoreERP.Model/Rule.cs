using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class Rule
    {
        public int id_regla { get; set; }
        public string regla { get; set; }
     

        public int dias_vencidos { get; set; }

     

        public int fraccion_desde { get; set; }
        public int fraccion_hasta { get; set; }

        public int mes_atraso { get; set; }
        public string tipo { get; set; }

        public int id_mensaje { get; set; }

        public string titulo { get; set; }
    }
}
