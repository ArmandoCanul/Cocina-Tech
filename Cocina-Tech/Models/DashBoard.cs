using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Cocina_Tech.BO;

namespace Cocina_Tech.Models
{
    public class DashBoard
    {
        ConexionModel ObjConex = new ConexionModel();


        public DataTable VerTabla()
        {
            string Con_Sql = string.Format("SELECT ID, NOMBREPLATILLO, ESTADO, IDESTADO from PEDIDO where ESTADO = 0");
            return ObjConex.TablaConsulta(Con_Sql);
        }

        public DataTable VerTablaAprobados()
        {
            string Con_Sql = string.Format("SELECT ID, NOMBREPLATILLO, PEDIDO.ESTADO, NOMBRE  FROM PEDIDO INNER JOIN ESTADOPEDIDO on PEDIDO.IDESTADO = ESTADOPEDIDO.IDEstado WHERE PEDIDO.ESTADO = 1");
            return ObjConex.TablaConsulta(Con_Sql);
        }

        public int Aprobar(PedidoBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE PEDIDO SET ESTADO = 1 WHERE ID = @ID");
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ObjBO.ID;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public int Eliminar(PedidoBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[PEDIDO] WHERE ID = @ID");
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ObjBO.ID;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public int EstadoPreparando(PedidoBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE PEDIDO SET IDESTADO = 4 WHERE ID = @ID");
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ObjBO.ID;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public int EstadoEnCamino(PedidoBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE PEDIDO SET IDESTADO = 5 WHERE ID = @ID");
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ObjBO.ID;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public int EstadoPendiente(PedidoBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE PEDIDO SET IDESTADO = 1 WHERE ID = @ID");
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ObjBO.ID;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public int EstadoEntregado(PedidoBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE PEDIDO SET IDESTADO = 3 WHERE ID = @ID");
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ObjBO.ID;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }
    }
}