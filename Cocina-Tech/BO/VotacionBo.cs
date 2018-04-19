using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cocina_Tech.BO
{
    public class VotacionBo
    {
        public int ID { get; set; }
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string Preparacion { get; set; }
        public DateTime Tiempo { get; set; }
        public Boolean Estado { get; set; }
        public string Precio { get; set; }
        public int IDusuario { get; set; }
        public int IDCategoria { get; set; }
        public byte[] Portada { get; set; }
        public string Descripcion { get; set; }

    }
}