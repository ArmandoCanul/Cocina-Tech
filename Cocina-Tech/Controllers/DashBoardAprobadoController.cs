using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cocina_Tech.Models;
using Cocina_Tech.BO;

namespace Cocina_Tech.Controllers
{
    public class DashBoardAprobadoController : Controller
    {
        DashBoard ObjModel = new DashBoard();
        // GET: DashBoardAprobado
        public ActionResult DashBoardAprobado()
        {
            return View(ObjModel.VerTablaAprobados());
        }

        public ActionResult EstadoEnCamino(string ID)
        {
            PedidoBO ObjBO = new PedidoBO();
            ObjBO.ID = int.Parse(ID);
            ObjModel.EstadoEnCamino(ObjBO);
            return RedirectToAction("DashBoard", "DashBoard");
        }

        public ActionResult EstadoEntregado(string ID)
        {
            PedidoBO ObjBO = new PedidoBO();
            ObjBO.ID = int.Parse(ID);
            ObjModel.EstadoEntregado(ObjBO);
            return RedirectToAction("DashBoard", "DashBoard");
        }

        public ActionResult EstadoPendiente(string ID)
        {
            PedidoBO ObjBO = new PedidoBO();
            ObjBO.ID = int.Parse(ID);
            ObjModel.EstadoPendiente(ObjBO);
            return RedirectToAction("DashBoard", "DashBoard");
        }

        public ActionResult EstadoPreparando(string ID)
        {
            PedidoBO ObjBO = new PedidoBO();
            ObjBO.ID = int.Parse(ID);
            ObjModel.EstadoPreparando(ObjBO);
            return RedirectToAction("DashBoard", "DashBoard");
        }
    }
}