using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cocina_Tech.BO
{
    public class Galeria
    {
        public int ID { get; set; }
        public byte[] Imagen { get; set; }
        public bool Estado{ set; get; }
        public DateTime Fecha { set; get; }
        public int Id_platillo { set; get; }
    }
}