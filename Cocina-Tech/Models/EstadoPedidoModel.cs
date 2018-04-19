using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Cocina_Tech.BO;

namespace Cocina_Tech.Models
{
    public class EstadoPedidoModel
    {
        ConexionModel ObjConex = new ConexionModel();

        public int AgregarEstado(EstadoPedidoBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("insert into ESTADOPEDIDO (NOMBRE, ESTADO)output inserted.IDEstado values (@NOMBRE, @ESTADO)");
            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = ObjBO.Nombre;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = ObjBO.Estado;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public DataTable VerTabla()
        {
            string Con_Sql = string.Format("select IDEstado, NOMBRE from ESTADOPEDIDO where ESTADO=1");
            return ObjConex.TablaConsulta(Con_Sql);
        }

        public int Eliminar(EstadoPedidoBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[ESTADOPEDIDO] WHERE IDEstado = @ID ");
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ObjBO.IDEstado;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }
    }
}