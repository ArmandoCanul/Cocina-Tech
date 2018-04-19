using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace Cocina_Tech.Models
{
    public class Respaldo
    {
        SqlConnection Conexion = new SqlConnection("Data Source=DESKTOP-FRKF6KH;Initial Catalog=PROYECTO;Integrated Security=True");
        public void RespaldoBD()
        {
            string nombre_copia = (System.DateTime.Today.Day.ToString() + "-" + System.DateTime.Today.Month.ToString() + "-" + System.DateTime.Today.Year.ToString() + "-" + System.DateTime.Now.Hour.ToString() + "-" + System.DateTime.Now.Minute.ToString() + "-" + System.DateTime.Now.Second.ToString() + " Cocina_Tech");
            string Comando_Consulta = "BACKUP DATABASE [PROYECTO] TO  DISK = N'C:\\Backup\\" + nombre_copia + ".bak' WITH NOFORMAT, NOINIT,  NAME = N'PROYECTO-Completa Base de datos Copia de seguridad', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            SqlCommand cmd = new SqlCommand(Comando_Consulta, Conexion);
            Conexion.Open();
            cmd.ExecuteNonQuery();
            Conexion.Close();
            Conexion.Dispose();
}
    }
 }