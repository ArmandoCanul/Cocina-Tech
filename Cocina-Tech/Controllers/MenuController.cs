using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cocina_Tech.Models;

namespace Cocina_Tech.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        PlatilloModel objModel = new PlatilloModel();
        public ActionResult Menu()
        {
            return View(objModel.PlatilloPortada().Tables[0]);
        }

        public ActionResult DetalleMenu(int ID)
        {
            Session["IDIN"]= ID;
            return View(objModel.PlatilloDetalle(ID).Tables[0]);
        }

        [ChildActionOnly]
        public ActionResult Ingrediente()
        {
            int id = int.Parse(Session["IDIN"].ToString());
            return View(objModel.ingredienteDetalle(id));
        }
    }
}