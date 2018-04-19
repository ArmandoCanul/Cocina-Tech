using Cocina_Tech.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Cocina_Tech.Models
{
    public class PlatilloModel
    {
        ConexionModel ObjConex = new ConexionModel();


        public int AgregarPlatillo(PlatilloBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("insert into PLATILLO (NOMBRE, RECETARIO, TIEMPODEPREPARACION, FECHACREACIONPLATILLO, Estado,IDCATEGORIA, PRECIO, PORTADA, IDUSUARIO)output inserted.ID values (@NOMBRE, @RECETARIO, @TIEMPODEPREPARACION, @FECHACREACIONPLATILLO, @ESTADO, @IDCATEGORIA, @PRECIO, @Portada, @IDUSUARIO)");
            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = ObjBO.Nombre;
            cmd.Parameters.Add("@RECETARIO", SqlDbType.Image).Value = ObjBO.Recetario;
            cmd.Parameters.Add("@TIEMPODEPREPARACION", SqlDbType.VarChar).Value = ObjBO.TiempoPreparacion;
            cmd.Parameters.Add("@FECHACREACIONPLATILLO", SqlDbType.Date).Value = ObjBO.FechaCreacion;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = ObjBO.Estado;
            cmd.Parameters.Add("@IDCATEGORIA", SqlDbType.Int).Value = ObjBO.IDC;
            cmd.Parameters.Add("@PRECIO", SqlDbType.VarChar).Value = ObjBO.Precio;
            cmd.Parameters.Add("@Portada", SqlDbType.Image).Value = ObjBO.Portada;
            cmd.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = ObjBO.IDUSUARIO;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public int AgregarGaleria(Galeria objBO)
        {
            SqlCommand cmd = new SqlCommand("insert into RECURSOS (Imagen, ESTADO, FECHACREACION, IDPLATILLO) values (@imagen, @estado, @Fecha, @idplatillo )");
            cmd.Parameters.Add("@imagen", SqlDbType.Image).Value = objBO.Imagen;
            cmd.Parameters.Add("@estado", SqlDbType.Bit).Value = objBO.Estado;
            cmd.Parameters.Add("@Fecha", SqlDbType.Image).Value = objBO.Fecha;
            cmd.Parameters.Add("@idplatillo", SqlDbType.Int).Value = objBO.Id_platillo;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }
        public int Modificar(PlatilloBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE PLATILLO SET (NOMBRE = @NOMBRE), (TIEMPODEPREPARACION = @TIEMPODEPREPARACION), (FECHACREACIONPLATILLO = @FECHACREACIONPLATILLO) WHERE ID = @ID");
            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = ObjBO.Nombre;
            cmd.Parameters.Add("@TIEMPODEPREPARACION", SqlDbType.VarChar).Value = ObjBO.TiempoPreparacion;
            cmd.Parameters.Add("@FECHACREACIONPLATILLO", SqlDbType.VarChar).Value = ObjBO.FechaCreacion;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ObjBO.ID;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public int Eliminar(PlatilloBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[PLATILLO] WHERE ID = @ID ");
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ObjBO.ID;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public DataTable Recuperar()
        {
            return ObjConex.TablaConsulta("SELECT ID, NOMBRE, RECETARIO, TIEMPODEPREPARACION, FECHACREACIONPLATILLO FROM PLATILLO");
            
        }

      public DataSet TABLA_PLATILLO()
        {
            SqlCommand cmd = new SqlCommand("select ID, NOMBRE, TIEMPODEPREPARACION, FECHACREACIONPLATILLO, ESTADO, PRECIO from PLATILLO WHERE ESTADO=1");
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);
        }

        public DataSet TABLA_PLATILLOID(int ID)
        {
            SqlCommand cmd = new SqlCommand("select ID, NOMBRE, TIEMPODEPREPARACION, FECHACREACIONPLATILLO, ESTADO, PRECIO from PLATILLO WHERE ESTADO=1 AND ID="+ID);
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);
        }

        public List<CategoriaBO> listaCat()
        {
            string query = ("select ID, NOMBRE from CATEGORIA WHERE ESTADO=1");
            var result = ObjConex.TablaConsulta(query);
            List<CategoriaBO> ListaCat = new List<CategoriaBO>();
            foreach (DataRow Medida in result.Rows)
            {
                var catbo = new CategoriaBO();
               catbo.IDCT = int.Parse(Medida[0].ToString());
                catbo.Nombre = Medida[1].ToString();
                ListaCat.Add(catbo);
            }
            return ListaCat;
        }

        public DataSet PlatilloPortada()
        {
            SqlCommand cmd = new SqlCommand("select ID, NOMBRE, PRECIO, PORTADA from PLATILLO where ESTADO= 1");
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);
        }


        public DataTable Tablaingred(int busqueda)
        {
            return ObjConex.TablaConsulta("SELECT NOMBRE, CANTIDAD FROM Ingredientexplat where IDPLATILLO="+busqueda);

        }

        public int Agregarcantidad(PlatilloBO objBO)
        {
            SqlCommand cmd = new SqlCommand("insert into INGREDIENTESPARAPLATILLO (CANTIDAD, IDINGREDIENTES, IDPLATILLO) values (@cantidad, @idingredientes, @idplatillo )");
            cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = objBO.cantidad;
            cmd.Parameters.Add("@idingredientes", SqlDbType.Int).Value = objBO.IDINGREDIENTE;
            cmd.Parameters.Add("@idplatillo", SqlDbType.Int).Value = objBO.ID;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public DataSet PlatilloDetalle(int busqueda)
        {
            SqlCommand cmd = new SqlCommand("select i.ID as IDINGREDIENTE, i.NOMBRE as INGREDIENTE, ig.CANTIDAD, m.MEDIDA as MEDIDA, p.NOMBRE as Platillo, p.ID as IDPLATILLO, p.FECHACREACIONPLATILLO, p.PRECIO, p.ESTADO, P.PORTADA, C.NOMBRE as CATEGORIA from INGREDIENTESPARAPLATILLO ig inner join INGREDIENTES i on ig.IDINGREDIENTES=i.ID inner join PLATILLO p on p.ID=ig.IDPLATILLO inner join MEDIDA m on i.IDMEDIDA=m.ID INNER JOIN  CATEGORIA C ON C.ID=P.IDCATEGORIA WHERE IDPLATILLO="+busqueda);
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);
        }

        public DataSet ingredienteDetalle(int busqueda)
        {
            SqlCommand cmd = new SqlCommand("select i.NOMBRE, ig.IDPLATILLO from INGREDIENTES i inner join INGREDIENTESPARAPLATILLO ig on i.ID=ig.IDINGREDIENTES WHERE IDPLATILLO=" + busqueda);
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);
        }

        public DataSet mostrarCaliP()
        {

            SqlCommand cmd = new SqlCommand("SELECT ID,NOMBRE,TIEMPODEPREPARACION,FECHACREACIONPLATILLO,ESTADO,PRECIO,MALA,BUENA FROM CALIFICACIONP where ESTADO=1");
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);
        }
        public DataSet PlatilloTemporal()
        {
            SqlCommand cmd = new SqlCommand("select PlatilloTemp.ID_TEM,PlatilloTemp.NOMBRE,DESCRIPCION,TIEMPODEPREPARACION,FECHACREACION,ESTADO,  ( select COUNT(*) from PlatilloCali pc where pc.ID_TEM=PlatilloTemp.ID_TEM) AS NumeroVotos from PlatilloTemp ");
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);
        }

        public DataSet AgregarPlatilloTem(VotacionBo ObjBO)
        {
            SqlCommand cmd = new SqlCommand("insert into PlatilloTemp (NOMBRE, TIEMPODEPREPARACION, FECHACREACION, Estado, PRECIO, IDCATEGORIA, IDUSUARIO, PORTADA, DESCRIPCION) values (@NOMBRE, @TIEMPODEPREPARACION, @FECHACREACION, @ESTADO, @PRECIO, @IDCATEGORIA, @IDUSUARIO, @Portada, @DESCRIPCION)");
            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = ObjBO.Nombre;
            cmd.Parameters.Add("@TIEMPODEPREPARACION", SqlDbType.VarChar).Value = ObjBO.Preparacion;
            //cmd.Parameters.Add("@FECHACREACIONPLATILLO", SqlDbType.VarChar).Value = ObjBO.FechaCreacion;
            cmd.Parameters.Add("@FECHACREACION", SqlDbType.DateTime).Value = ObjBO.FechaCreacion;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = ObjBO.Estado;
            cmd.Parameters.Add("@PRECIO", SqlDbType.VarChar).Value = ObjBO.Precio;
            cmd.Parameters.Add("@IDCATEGORIA", SqlDbType.Int).Value = ObjBO.IDCategoria;
            cmd.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = ObjBO.IDusuario;
            cmd.Parameters.Add("@PORTADA", SqlDbType.Image).Value = ObjBO.Portada;
            cmd.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar).Value = ObjBO.Descripcion;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);
        }
        public int ActualizarEstatus(VotacionBo ObjBO)
        {
            SqlCommand cmd = new SqlCommand("update PlatilloTemp set ESTADO=@ESTADO where ID_TEM=@id");
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = ObjBO.Estado;
            cmd.Parameters.Add("@id", SqlDbType.Int, 50).Value = ObjBO.ID;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);

        }

        public DataSet PlatilloVotacion()
        {
            SqlCommand cmd = new SqlCommand("select * from PlatilloTemp where ESTADO=0;");
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);
        }

        public int Votar(int IdT, int IDUs)
        {
            SqlCommand cmd = new SqlCommand("insert into PlatilloCali( ID_TEM, ID_USUARIO) values (@temp, @usua)");
            cmd.Parameters.Add("@temp", SqlDbType.Int).Value = IdT;
            cmd.Parameters.Add("@usua", SqlDbType.Int).Value = IDUs;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public int ActualizarVotacion(VotacionBo OBJBO)
        {

            SqlCommand cmd = new SqlCommand("Update Usuarios Set NOMBRE=@Nombre, [TIEMPODEPREPARACION]=@tiempo, FECHACREACION=@fecha, [PRECIO]=@precio, IDCATEGORIA=@idcat, IDUSUARIO=@idusu, PORTADA=@foto, DESCRIPCION=@descripcion Where ID_TEM=@Id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = OBJBO.ID;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = OBJBO.Nombre;
            cmd.Parameters.Add("@tiempo", SqlDbType.VarChar).Value = OBJBO.Preparacion;
            cmd.Parameters.Add("@fecha", SqlDbType.VarChar).Value = OBJBO.FechaCreacion;
            cmd.Parameters.Add("@precio", SqlDbType.DateTime).Value = OBJBO.Precio;
            cmd.Parameters.Add("@idcat", SqlDbType.VarChar).Value = OBJBO.IDCategoria;
            cmd.Parameters.Add("@idusua", SqlDbType.VarChar).Value = OBJBO.IDusuario;
            cmd.Parameters.Add("@foto", SqlDbType.VarChar).Value = OBJBO.Portada;
            cmd.Parameters.Add("@descripcion", SqlDbType.Image).Value = OBJBO.Descripcion;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarComando(cmd);
        }

        public DataSet VotoPlatillo(int id)
        {
            SqlCommand cmd = new SqlCommand("select*from PlatilloTemp t inner join PlatilloCali v on t.ID_TEM = v.ID_TEM inner join  USUARIOS u on u.ID = v.ID_USUARIO where v.ID_USUARIO = @ID");
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            cmd.CommandType = CommandType.Text;
            return ObjConex.EjecutarSentencia(cmd);

        }

    }
}
