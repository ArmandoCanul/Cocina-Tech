using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Cocina_Tech.BO;
namespace Cocina_Tech.Models
{
    public class MensajeModel
    {
        ConexionModel ObjConexion = new ConexionModel();

        public int AgregarMensajeContacto(string Mensaje, int IDenvia, string Asunto)
        {
            SqlCommand cmd = new SqlCommand("insert into Mensajes (IDusuairoenvia, IDusuariorecibe, Mensaje, Asunto, Fecha, Estatus) values (@IDenvia, @IDrecibe, @Mensaje, @Asunto, @Fecha, @Estatus)");
            cmd.Parameters.Add("@IDenvia", SqlDbType.Int).Value = IDenvia;
            cmd.Parameters.Add("@IDrecibe", SqlDbType.Int).Value = 1;
            cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar).Value = Mensaje;
            cmd.Parameters.Add("@Asunto", SqlDbType.VarChar).Value = Asunto;
            cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Estatus", SqlDbType.Int).Value = 0;
            cmd.CommandType = CommandType.Text;
            return ObjConexion.EjecutarComando(cmd);
        }
        public int ActualizarEstado(int ID)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Mensajes SET Estatus=@estado  WHERE ID = @ID");
            cmd.Parameters.Add("@estado", SqlDbType.Int).Value = 1;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            cmd.CommandType = CommandType.Text;
            return ObjConexion.EjecutarComando(cmd);
        }
        public DataSet MensajesRecibidos()
        {
            SqlCommand cmd = new SqlCommand("select * from Bandeja where IDusuariorecibe = 1 and Estatus = 0");
            cmd.CommandType = CommandType.Text;
            return ObjConexion.EjecutarSentencia(cmd);
        }
        public DataSet MensajesLeidosAdmin()
        {
            SqlCommand cmd = new SqlCommand("select * from Bandeja where IDusuairoenvia = 1 and Estatus = 1");
            cmd.CommandType = CommandType.Text;
            return ObjConexion.EjecutarSentencia(cmd);
        }

        public DataSet MensajesRecibidosUsuario(int ID)
        {
            SqlCommand cmd = new SqlCommand("select * from Bandeja where IDusuariorecibe =" + ID + "and Estatus = 0");
            cmd.CommandType = CommandType.Text;
            return ObjConexion.EjecutarSentencia(cmd);
        }

        public DataSet vermensaje(int ID)
        {
            SqlCommand cmd = new SqlCommand("select * from Bandeja where ID=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = ID;
            cmd.CommandType = CommandType.Text;
            return ObjConexion.EjecutarSentencia(cmd);
        }

        public int Mensajeenviado(string Mensaje, int IDenvia, string Asunto, string IDalgo)
        {
            SqlCommand cmd = new SqlCommand("insert into Mensajes (IDusuairoenvia, IDusuariorecibe, Mensaje, Asunto, Fecha, Estatus) values (@IDenvia, @IDrecibe, @Mensaje, @Asunto, @Fecha, @Estatus)");
            cmd.Parameters.Add("@IDenvia", SqlDbType.Int).Value = IDenvia;
            cmd.Parameters.Add("@IDrecibe", SqlDbType.Int).Value = Convert.ToInt32(IDalgo);
            cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar).Value = Mensaje;
            cmd.Parameters.Add("@Asunto", SqlDbType.VarChar).Value = Asunto;
            cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Estatus", SqlDbType.Int).Value = 1;
            cmd.CommandType = CommandType.Text;
            return ObjConexion.EjecutarComando(cmd);
        }



        public int NumeroMensaje()
        {
            SqlCommand cmd = new SqlCommand("select count(*) from Mensajes where Estatus=0");
            cmd.CommandType = CommandType.Text;
            DataSet ds = ObjConexion.EjecutarSentencia(cmd);
            string M = ds.Tables[0].Rows[0][0].ToString();
            return Convert.ToInt32(M);
        }
    }
 }