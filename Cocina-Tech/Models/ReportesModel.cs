using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace Cocina_Tech.Models
{
    public class ReportesModel
    {

        ConexionModel ObjConex = new ConexionModel();

        public DataTable Usuarios()
        {
            return ObjConex.TablaConsulta("SELECT NOMBRE, APELLIDOS, USUARIO, CORREO FROM USUARIOS WHERE IDTIPO = 2");
        }

        public DataTable UsuarioChef()
        {
            return ObjConex.TablaConsulta("SELECT NOMBRE, APELLIDOS, USUARIO, CORREO FROM USUARIOS WHERE IDTIPO = 3");
        }

        public DataTable Ingredientes()
        {
            return ObjConex.TablaConsulta("SELECT NOMBRE, FECHACREACION FROM INGREDIENTES");
        }
    }
}