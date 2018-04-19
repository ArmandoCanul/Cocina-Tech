using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cocina_Tech.BO
{
    public class MensajesBO
    {
        public int IDrecibe { get; set; }
        public int IDenvia { get; set; }
        public int ID { get; set; }
        public string Mensaje { get; set; }
        public string Asunto { get; set; }
        public DateTime Fecha { get; set; }

        public int Estatus { get; set; }
    }
}