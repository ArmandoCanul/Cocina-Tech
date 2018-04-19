using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cocina_Tech.BO
{
    public class PedidoBO
    {
        public int ID { get; set; }
        public double TOTAL { get; set; }
        public DateTime FECHAPEDIDO { get; set; }
        public TimeSpan HORAPEDIDO { get; set; }
        public TimeSpan HORAENTREGA { get; set; }
        public bool ESTADO { get; set; }

        public string Usuario { get; set; }

        public List<DetallePedidoBO> DETALLES { get; set; }

    }

    public class DetallePedidoBO
    {
        public int ID { get; set; }
        public string NOMBRE { get; set; }
        public string PRECIO { get; set; }
    }
}