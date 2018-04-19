using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cocina_Tech.BO
{
    public class IngredientePrin
    {
        public int IDIn { get; set; }
        public string Nombre { get; set; }
        public Boolean EstadoI { get; set; }
        public DateTime Feha { get; set; }
        public int idMedida { get; set; }


        public List<IngredienteM> Medidas { get; set; }
    }
}