using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cocina_Tech.Models;
using Cocina_Tech.BO;
using System.Data;

namespace Cocina_Tech.Controllers.Usuario
{
    public class AdminInicioController : Controller
    {
        Respaldo ObjRespaldo = new Respaldo();
        MensajeModel mensobj = new MensajeModel();
        Registro regisUsu = new Registro();
        UsuarioBO objbo = new UsuarioBO();
        PlatilloModel objMP = new PlatilloModel();
        // GET: AdminInicio
        public ActionResult InicioAdmin()
        {
            if (Session["TipoUsuario"] == null)
            {
                return RedirectToAction("LoginP", "Login");
            }
            else if (Convert.ToInt32(Session["TipoUsuario"]) == 1)
            {
             
                return View();
            }
            else
            {
                return RedirectToAction("LoginP", "Login");
            }
            
        }

        public ActionResult ActualizarAdmin( string Nombre, string Apellidos, string Telefono, DateTime FechaNaci, string Correo, string Usuario, string Contraseña, HttpPostedFileBase Fotografia)
        {
            UsuarioBO ObjBO = new UsuarioBO();
            ObjBO.Nombre = Nombre;
            ObjBO.Apellido = Apellidos;
            ObjBO.Telefono = Telefono;
            ObjBO.FechaNac = FechaNaci;
            ObjBO.Correo = Correo;
            ObjBO.Usuario = Usuario;
            ObjBO.Contraseña = Contraseña;
            if (Fotografia != null && Fotografia.ContentLength > 0)
            {
                string imgP = Fotografia.FileName;
                ObjBO.Fotografia = new byte[Fotografia.ContentLength];
                Fotografia.InputStream.Read(ObjBO.Fotografia, 0, Fotografia.ContentLength);

                
            }
            //ObjBO.Fotografia = Fotografia;
            ObjBO.ID= int.Parse(Session["Usuario"].ToString());
            regisUsu.ActualizarUsuario(ObjBO);
            InicioAdmin();
            return RedirectToAction("InicioAdmin");
        }


        public ActionResult InboxAdmin()
        {
            ViewBag.Count = mensobj.NumeroMensaje();
            return View(mensobj.MensajesRecibidos().Tables[0]);
        }

        public ActionResult Respaldo()
        {
            ObjRespaldo.RespaldoBD();
            return View("InicioAdmin");
        }

        public ActionResult ResponderAdmin(int ID)
        {
            ViewBag.Count = mensobj.NumeroMensaje();
            Session["IDm"] = ID;
            return View(mensobj.vermensaje(ID).Tables[0]);
        }
       
        public ActionResult ResponderAdmin1(string Mensaje, string Asunto, string idenvia2)
        {
            int idenvia = Convert.ToInt32(Session["Usuario"]);
            MensajeModel mensobj = new MensajeModel();
            mensobj.Mensajeenviado(Mensaje, idenvia, Asunto, idenvia2);
            mensobj.ActualizarEstado(int.Parse(Session["IDm"].ToString()));
            InboxAdmin();

            return View("InboxAdmin");
        }

        public ActionResult InicioAdmin1()
        {
            int clave = Convert.ToInt32(Session["Usuario"]);
            return View(regisUsu.RecuperaUsuario(clave).Tables[0]);
        }

        public ActionResult MensajesLeidos()
        {
            ViewBag.Count = mensobj.NumeroMensaje();
            return View(mensobj.MensajesLeidosAdmin().Tables[0]);
        }

        public ActionResult CalificacionPlatillo()
        {
            return View(objMP.mostrarCaliP().Tables[0]);
        }
    }
}

























