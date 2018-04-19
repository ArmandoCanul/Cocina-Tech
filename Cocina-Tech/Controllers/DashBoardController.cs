using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cocina_Tech.Models;
using Cocina_Tech.BO;

namespace Cocina_Tech.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        DashBoard ObjModel = new DashBoard();
        public ActionResult DashBoard()
        {
            return View(ObjModel.VerTabla());
        }

        public ActionResult Aprobar(string ID)
        {
            PedidoBO ObjBO = new PedidoBO();
            ObjBO.ID = int.Parse(ID);
            ObjModel.Aprobar(ObjBO);
            return RedirectToAction("DashBoard", "DashBoard");
        }

        public ActionResult Eliminar(string ID)
        {
            PedidoBO ObjBO = new PedidoBO();
            ObjBO.ID = int.Parse(ID);
            ObjModel.Eliminar(ObjBO);
            return RedirectToAction("DasBoard", "DashBoard");
        }        
    }
}