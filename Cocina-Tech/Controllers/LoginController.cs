using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cocina_Tech.BO;
using Cocina_Tech.Models;
using System.Data.SqlClient;
using System.Data;

namespace Cocina_Tech.Controllers
{
    public class LoginController : Controller
    {
        Registro ObjModel = new Registro();
        // GET: Login
        int ID;
        public ActionResult Loggin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registro(string Nombre, string Apellidos, string Telefono, DateTime FechaNac, string Correo, string Usuario, string Contraseña, HttpPostedFileBase Fotografia)
        {
            UsuarioBO ObjBO = new UsuarioBO();
            ObjBO.Nombre = Nombre;
            ObjBO.Apellido = Apellidos;
            ObjBO.Telefono = Telefono;
            ObjBO.FechaNac = FechaNac;
            ObjBO.Correo = Correo;
            ObjBO.Usuario = Usuario;
            ObjBO.Contraseña = Contraseña;

            if (Fotografia != null && Fotografia.ContentLength > 0)
            {
                string imgP = Fotografia.FileName;
                ObjBO.Fotografia = new byte[Fotografia.ContentLength];
                Fotografia.InputStream.Read(ObjBO.Fotografia, 0, Fotografia.ContentLength);


            }
            ObjBO.estado = true;
            ObjModel.Agregar(ObjBO);

            return RedirectToAction("UsuarioPrincipal","Usuario");

        }

        public ActionResult LoginP()
        {
            ViewBag.id = ID;
            return View();
        }


        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult Loguearse(string Usuario, string Contraseña)
        {
            try
            {

                UsuarioBO ObjBO = new UsuarioBO();
                ObjBO.Usuario = Usuario;
                ObjBO.Contraseña = ObjBO.Encriptar(Contraseña);
                //ObjModel.Login(ObjBO);
               
                    DataTable Tabla = ObjModel.Login(ObjBO).Tables[0];
                    //if (Tabla.Rows.Count > 0)
                    //{

                    
                    int tipo = int.Parse(Tabla.Rows[0]["IDTIPO"].ToString());
                    int id = Convert.ToInt32(Tabla.Rows[0]["ID"]);
                    Session["Usuario"] = id;
                    Session["TipoUsuario"] = tipo;
                //}

                    
                if (Convert.ToInt32(Session["TipoUsuario"]) == 1)
                    {
                        DataTable tabla = ObjModel.datos(id).Tables[0];
                        Session["Nombre_usu"] = tabla.Rows[0]["Usuario"].ToString();
                    Session["Foto"] = (byte[])tabla.Rows[0]["FOTOGRAFIA"];
                    ID = 1;

                        return View("~/Views/AdminInicio/InicioAdmin.cshtml");

                    }
                    if (Convert.ToInt32(Session["TipoUsuario"]) == 2)
                    {

                        DataTable tabla = ObjModel.datos(id).Tables[0];
                        Session["Nombre_usu"] = tabla.Rows[0]["Usuario"].ToString();
                        Session["Nombre"] = tabla.Rows[0]["Nombre"].ToString();
                    Session["Foto"] = (byte[])tabla.Rows[0]["FOTOGRAFIA"];

                    return View("~/Views/Principal/UsuarioPrincipal.cshtml");

                }
                if (Convert.ToInt32(Session["TipoUsuario"]) == 3)
                {

                    DataTable tabla = ObjModel.datos(id).Tables[0];
                    Session["Nombre_usu"] = tabla.Rows[0]["Usuario"].ToString();
                    Session["Nombre"] = tabla.Rows[0]["Nombre"].ToString();
                    Session["Foto"] = (byte[])tabla.Rows[0]["FOTOGRAFIA"];

                    return View("~/Views/Chefinicio/InicioChef.cshtml");

                }


            }
            catch
            {

            }

            return View("~/Views/Principal/UsuarioPrincipal.cshtml");




        }
        

        public ActionResult UsuarioPrincipal()
        {
            return View("~/Views/Principal/UsuarioPrincipal.cshtml");
        }



        public ActionResult RegistrarUsuario()
        {
            return View();
        }
   }
}
