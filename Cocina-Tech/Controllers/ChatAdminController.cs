using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cocina_Tech.Models;
namespace Cocina_Tech.Controllers
{
    public class ChatAdminController : Controller
    {
        // GET: ChatAdmin
        public ActionResult ChatAdmin()
        {
            return View();
        }

        public ActionResult Contacto()
        {
            return View();
        }
        public ActionResult ContactoA(string Asunto, string Mensaje)
        {
            int idenvia = Convert.ToInt32(Session["Usuario"]);
            MensajeModel mensobj = new MensajeModel();
            mensobj.AgregarMensajeContacto(Mensaje, idenvia, Asunto);
            Contacto();
            return View("Contacto");
        }
    }
}