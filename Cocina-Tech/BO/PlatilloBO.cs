using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cocina_Tech.BO
{
    public class PlatilloBO
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public byte[] Recetario { get; set; }
        public string TiempoPreparacion { get; set; }    
        public DateTime FechaCreacion { get; set; }
        public string Precio { get; set; }
        public bool Estado { get; set; }
        public int IDC { get; set; }
        public int IDUSUARIO { get; set; }
        public byte[] Portada { get; set; }

        public int cantidad { get; set; }

        public int IDINGREDIENTE { get; set; }
     

        public List<CategoriaBO> Categorias { get; set; }




    }
}