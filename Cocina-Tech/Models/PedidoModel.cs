using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Cocina_Tech.Models
{
    public class PedidoModel
    {
        ConexionModel ObjConex = new ConexionModel();
        public DataTable ObtenerPedido(int Id)
        {
            return ObjConex.TablaConsulta("SELECT ID, TOTAL, FECHAPEDIDO, HORAPEDIDO, HORAENTREGA, ESTADO FROM PEDIDO WHERE IDUSUARIOS = " + Id);
        }

        public DataTable ObtenerDetallePedido(int Id)
        {
            return ObjConex.TablaConsulta("SELECT pla.ID as ID, pla.NOMBRE as NOMBRE, pla.PRECIO as PRECIO FROM DETALLEPEDIDO dp INNER JOIN PLATILLO pla on dp.IDPLATILLO = pla.ID  WHERE dp.IDPEDIDO = " + Id);
        }

        public DataTable ObtenerPedidoGeneral()
        {
            return ObjConex.TablaConsulta("SELECT U.NOMBRE,P.ID, P.TOTAL, P.FECHAPEDIDO, P.HORAPEDIDO, P.HORAENTREGA, P.ESTADO FROM PEDIDO P " +
                "INNER JOIN USUARIOS U ON P.IDUSUARIOS=U.ID");
        }
    }
}