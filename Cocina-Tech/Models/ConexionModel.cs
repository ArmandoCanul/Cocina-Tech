using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace Cocina_Tech.Models
{
    public class ConexionModel
    {
        SqlCommand ComandoSQL;
        SqlDataAdapter Adaptador;
        DataSet DataSetAdaptador;
        DataTable TablaVirtual;
        SqlConnection con;
        public SqlConnection establecerConexion()
        {
            //string cs = "Data Source=JESUS\\JESUS;Initial Catalog=PROYECTO;Integrated Security=True";
            string cs = "Data Source=SQL5027.site4now.net; Initial Catalog = DB_A361F1_ivangarcia82; User Id = DB_A361F1_ivangarcia82_admin; Password = ivanalexis11;";
            //string cs = "Data Source=DESKTOP-LJIVHQ1\\IVAN;Initial Catalog=PROYECTO;Integrated Security=True";
            con = new SqlConnection(cs);
            return con;
        }
        public SqlConnection establecerConexion(string conexionString)
        {
            string cs = conexionString;
            con = new SqlConnection(cs);
            return con;
        }
        public void abrirConexion() { con.Open(); }
        public void cerrarConexion() { con.Close(); }
        public int EjecutarComando(SqlCommand SqlComando)
        {
            // INSERT, DELETE, UPDATE
            ComandoSQL = new SqlCommand();
            ComandoSQL = SqlComando;
            ComandoSQL.Connection = this.establecerConexion();
            this.abrirConexion();
            int id = 0;
            id = Convert.ToInt32(ComandoSQL.ExecuteScalar());
            this.cerrarConexion();
            return id;
        }
        public DataSet EjecutarSentencia(SqlCommand SqlComando)
        {
            Adaptador = new SqlDataAdapter();
            ComandoSQL = new SqlCommand();
            DataSetAdaptador = new DataSet();
            ComandoSQL = SqlComando;
            ComandoSQL.Connection = this.establecerConexion();
            this.abrirConexion();
            Adaptador.SelectCommand = ComandoSQL;
            Adaptador.Fill(DataSetAdaptador);
            this.cerrarConexion();
            return DataSetAdaptador;
        }


        public DataTable TablaConsulta (string Sentencia)
        {
            Adaptador = new SqlDataAdapter(Sentencia, establecerConexion());
            TablaVirtual = new DataTable();
            Adaptador.Fill(TablaVirtual);
            return TablaVirtual;
        }
    }
}