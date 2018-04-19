using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Cocina_Tech.Models
{
    public class PedidoUsuario
    {
        ConexionModel ObjConex = new ConexionModel();
        public DataTable VerTabla()
        {
            string Con_Sql = string.Format("select IDEstado, NOMBRE from ESTADOPEDIDO where ESTADO=1");
            return ObjConex.TablaConsulta(Con_Sql);
        }
    }
}