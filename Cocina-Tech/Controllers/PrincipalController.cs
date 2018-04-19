using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cocina_Tech.Controllers
{
    public class PrincipalController : Controller
    {
        // GET: Principal
        public ActionResult InicioPrincipal()
        {
            Session.RemoveAll();
            return View();
        }

        public ActionResult ContactoInicio()
        {
            return View();
        } 

        public ActionResult Nosotros()
        {
            return View();
        }

    }
}