using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Cocina_Tech.BO;

namespace Cocina_Tech.Models
{
    public class Registro
    {
        ConexionModel ObjConex = new ConexionModel();

        public int Agregar(UsuarioBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("insert into USUARIOS (NOMBRE, APELLIDOS, TELEFONO, FECHANACIMIENTO, CORREO, USUARIO, CONTRASEÑA, IDTIPO, ESTADO) values (@NOMBRE, @APELLIDOS, @TELEFONO, @FECHANACIMIENTO, @CORREO, @USUARIO, @CONTRASEÑA, @IDTIPO, @ESTADO)");
            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = ObjBO.Nombre;
            cmd.Parameters.Add("@APELLIDOS", SqlDbType.VarChar).Value = ObjBO.Apellido;
            cmd.Parameters.Add("@TELEFONO", SqlDbType.VarChar).Value = ObjBO.Telefono;
            cmd.Parameters.Add("@FECHANACIMIENTO", SqlDbType.DateTime).Value = ObjBO.FechaNac;
            cmd.Parameters.Add("@CORREO", SqlDbType.VarChar).Value = ObjBO.Correo;
            cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = ObjBO.Usuario;
            cmd.Parameters.Add("@CONTRASEÑA", SqlDbType.VarChar).Value = ObjBO.Encriptar(ObjBO.Contraseña);
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = ObjBO.estado;
            cmd.Parameters.Add("@IDTIPO", SqlDbType.Int).Value = 2;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }


        public int AgregarChef(UsuarioBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("insert into USUARIOS (NOMBRE, APELLIDOS, TELEFONO, FECHANACIMIENTO, CORREO, USUARIO, CONTRASEÑA, FOTOGRAFIA,IDTIPO, ESTADO) values (@NOMBRE, @APELLIDOS, @TELEFONO, @FECHANACIMIENTO, @CORREO, @USUARIO, @CONTRASEÑA, @FOTO, @IDTIPO, @ESTADO)");
            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = ObjBO.Nombre;
            cmd.Parameters.Add("@APELLIDOS", SqlDbType.VarChar).Value = ObjBO.Apellido;
            cmd.Parameters.Add("@TELEFONO", SqlDbType.VarChar).Value = ObjBO.Telefono;
            cmd.Parameters.Add("@FECHANACIMIENTO", SqlDbType.DateTime).Value = ObjBO.FechaNac;
            cmd.Parameters.Add("@CORREO", SqlDbType.VarChar).Value = ObjBO.Correo;
            cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = ObjBO.Usuario;
            cmd.Parameters.Add("@CONTRASEÑA", SqlDbType.VarChar).Value = ObjBO.Encriptar(ObjBO.Contraseña);
            cmd.Parameters.Add("@FOTO", SqlDbType.Image).Value = ObjBO.Fotografia;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = ObjBO.estado;
            cmd.Parameters.Add("@IDTIPO", SqlDbType.Int).Value = 3;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public int EliminarChef(UsuarioBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("Update USUARIOS SET ESTADO=@ESTADO Where ID=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = ObjBO.ID;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = ObjBO.estado;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public DataSet Login(UsuarioBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("SELECT ID, IDTIPO, ESTADO FROM USUARIOS WHERE USUARIO = @Usuario AND Contraseña = @Contraseña AND ESTADO=1");
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = ObjBO.Usuario;
            cmd.Parameters.Add("@Contraseña", SqlDbType.VarChar).Value = ObjBO.Contraseña;
        
            cmd.CommandType = CommandType.Text;  
            return ObjConex.EjecutarSentencia(cmd);
        }

        public DataSet datos(int id)
        {
            SqlCommand cmd = new SqlCommand("Select * from USUARIOS where ID=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);
        }

        public int ActualizarUsuario(UsuarioBO OBJBO)
        {
            UsuarioBO objbo = new UsuarioBO();
            SqlCommand cmd = new SqlCommand("Update Usuarios Set Nombre=@Nombre, [APELLIDOS]=@Apellido, Telefono=@Telefono, [FECHANACIMIENTO]=@Fecha, Correo=@Correo, Usuario=@Usuario, Contraseña=@Contraseña, Fotografia=@Foto Where ID=@Id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value =OBJBO.ID;
            cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = OBJBO.Nombre;
            cmd.Parameters.Add("@Apellido", SqlDbType.VarChar).Value =OBJBO.Apellido;
            cmd.Parameters.Add("@Telefono", SqlDbType.VarChar).Value =OBJBO.Telefono;
            cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = OBJBO.FechaNac;
            cmd.Parameters.Add("@Correo", SqlDbType.VarChar).Value = OBJBO.Correo;
            cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = OBJBO.Usuario;
            cmd.Parameters.Add("@Contraseña", SqlDbType.VarChar).Value = objbo.Encriptar(OBJBO.Contraseña);
            cmd.Parameters.Add("@Foto", SqlDbType.Image).Value = OBJBO.Fotografia;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }


        public DataSet Tabla_Chef()
        {
            SqlCommand cmd = new SqlCommand("select ID, NOMBRE, APELLIDOS, TELEFONO, FECHANACIMIENTO, CORREO, USUARIO, ESTADO from USUARIOS where IDTIPO=3");
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);

        }



        public string RecuperarPerfil(int id) //SobreCarga Con el ID del usuario que ya ingreso
        {
            SqlCommand cmd = new SqlCommand("select Tipo  from USUARIOS, TIPOCUENTA_USUARIO where USUARIOS.IDTIPO = TIPOCUENTA_USUARIO.IDTIPO   and USUARIOS.ID = (ID)");
            cmd.Parameters.Add("ID", SqlDbType.Int, 50).Value = id;
            cmd.CommandType = CommandType.Text;
            DataSet datos = ObjConex.EjecutarSentencia(cmd);
            string Perfil = "";

            if (datos.Tables[0].Rows.Count > 0)
            {
                Perfil = datos.Tables[0].Rows[0]["Tipo"].ToString();
            }
            return Perfil;
        }

        public DataSet RecuperaUsuario(int id)
        {
            SqlCommand cmd = new SqlCommand("select u.ID, u.NOMBRE, u.APELLIDOS, u.TELEFONO, u.FECHANACIMIENTO, u.CORREO, u.USUARIO, u.CONTRASEÑA, u.FOTOGRAFIA from USUARIOS u where  u.ID=@ID");
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);

        }

        public DataSet Tabla_Usuario()
        {
            SqlCommand cmd = new SqlCommand("select ID, NOMBRE, APELLIDOS,TELEFONO, FECHANACIMIENTO, CORREO, USUARIO, ESTADO from USUARIOS where ESTADO=1");
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);

        }

        public int EliminarUsuario(UsuarioBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("Update USUARIOS SET ESTADO=@ESTADO Where ID=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = ObjBO.ID;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = ObjBO.estado;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }








    }
}