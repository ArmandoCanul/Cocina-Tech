using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cocina_Tech.BO;
using System.Data.SqlClient;
using System.Data;

namespace Cocina_Tech.Models
{
    public class EstadoPlatilloModel
    {
        ConexionModel ObjConex = new ConexionModel();

        public int AgregarEstado(EstadoPedidoBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("insert into ESTADOPEDIDO (NOMBRE, ESTADO)output inserted.ID values (@NOMBRE, @ESTADO)");
            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = ObjBO.Nombre;
            cmd.Parameters.Add("@RECETARIO", SqlDbType.Image).Value = ObjBO.Estado = true;          
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }
    }
}