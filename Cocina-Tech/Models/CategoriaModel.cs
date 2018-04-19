using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Cocina_Tech.BO;
namespace Cocina_Tech.Models
{
    public class CategoriaModel
    {
        ConexionModel ObjConexion = new ConexionModel();

        public int Agregar(CategoriaBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("insert into CATEGORIA (NOMBRE, FECHACREACION, ESTADO) values (@NOMBRE, @FECHACREACION, @ESTADO)");
            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = ObjBO.Nombre;
            cmd.Parameters.Add("@FECHACREACION", SqlDbType.Date).Value = ObjBO.FechaCreacion;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = ObjBO.Estado;
            cmd.CommandType = CommandType.Text;
            return ObjConexion.EjecutarComando(cmd);
        }

        public int EliminarCat(CategoriaBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("Update  CATEGORIA SET ESTADO=@ESTADO Where ID=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value=ObjBO.IDCT;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = ObjBO.Estado;
            cmd.CommandType = CommandType.Text;
            return ObjConexion.EjecutarComando(cmd);
        }
    

        public DataTable Tabla_Categoria_bd()
        {
            string Con_Sql = string.Format("select ID, NOMBRE, FECHACREACION, ESTADO from CATEGORIA where ESTADO=1");
            return ObjConexion.TablaConsulta(Con_Sql);
        }

        public int EstatusCategoria(CategoriaBO ObjBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE CATEGORIA SET ESTADO = @ESTADO WHERE ID = @ID");
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ObjBO.IDCT;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = ObjBO.Estado;
            return ObjConexion.EjecutarComando(cmd);
        }


        public int AgregarMedida(IngredienteM objBo)
        {
           SqlCommand cmd = new SqlCommand("insert into MEDIDA (MEDIDA, Estado) values (@MEDIDA,  @ESTADO)");
            cmd.Parameters.Add("@MEDIDA", SqlDbType.VarChar).Value = objBo.Medida;
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = objBo.EstadoM;
            return ObjConexion.EjecutarComando(cmd);

        }

        public DataSet Tabla_Medida()
        {
            SqlCommand cmd = new SqlCommand("select ID, MEDIDA, Estado from MEDIDA");
            cmd.CommandType = CommandType.Text;
            return ObjConexion.EjecutarSentencia(cmd);
        }

        public int AgregarIngrediente(IngredientePrin objBo)
        {
            SqlCommand cmd = new SqlCommand("insert into INGREDIENTES (NOMBRE, Estado, FECHACREACION, IDMEDIDA) values (@NOMBRE,  @ESTADO, @FECHA, @IDMEDIDA)");
            cmd.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = objBo.Nombre;
            cmd.Parameters.Add("@Estado", SqlDbType.Bit).Value = objBo.EstadoI;
            cmd.Parameters.Add("@FECHA", SqlDbType.Date).Value = objBo.Feha;
            cmd.Parameters.Add("@IDMEDIDA", SqlDbType.Int).Value = objBo.idMedida;
            return ObjConexion.EjecutarComando(cmd);
        }

        public int EliminarIng(IngredientePrin ObjBO)
        {
            SqlCommand cmd = new SqlCommand("Update INGREDIENTES SET ESTADO=@ESTADO Where ID=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = ObjBO.IDIn;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Bit).Value = ObjBO.EstadoI;
            cmd.CommandType = CommandType.Text;
            return ObjConexion.EjecutarComando(cmd);
        }


        public DataSet Tabla_Ingrediente()
        {
           SqlCommand cmd = new SqlCommand("select * from VistIngrediente");
            cmd.CommandType = CommandType.Text;
            return ObjConexion.EjecutarSentencia(cmd);
        }


        public List<IngredienteM> listaMedida()
        {
            string query = ("SELECT ID, MEDIDA FROM MEDIDA WHERE ESTADO = 1");
            var result = ObjConexion.TablaConsulta(query);
            List<IngredienteM> ListaMedida = new List<IngredienteM>();
            foreach (DataRow Medida in result.Rows)
            {
                var medidabo = new IngredienteM();
                medidabo.IDM = int.Parse(Medida[0].ToString());
                medidabo.Medida = Medida[1].ToString();
                ListaMedida.Add(medidabo);
            }
            return ListaMedida;
        }

    }
}