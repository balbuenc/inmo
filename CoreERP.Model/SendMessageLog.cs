using System;
using System.Collections.Generic;
using System.Text;

namespace CoreERP.Model
{
    public class SendMessageLog
	{
        public int id_envio_mensaje { get; set; }
        public DateTime fecha { get; set; }
        public int id_mensaje { get; set; }
        public string texto { get; set; }
        public int id_fraccion { get; set; }
        public string id_manzana { get; set; }
        public int id_lote { get; set; }

        public string numero_contrato { get; set; }
        public string nombre_para_documento { get; set; }
        public int id_cliente { get; set; }
        public string documento { get; set; }
        public int numero_cuota { get; set; }
        public decimal monto_cuota { get; set; }
        public decimal mora_cuota { get; set; }
        public DateTime fecha_vencimiento { get; set; }
        public string telefono_particular { get; set; }
        public string comando { get; set; }
        public string respuesta { get; set; }
    }
}
