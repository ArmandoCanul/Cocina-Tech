using Cocina_Tech.BO;
using Cocina_Tech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cocina_Tech.Controllers
{
    public class CategoriaController : Controller
    {
        CategoriaModel ObjModel = new CategoriaModel();
        // GET: Categoria
        public ActionResult Categoria()
        {
            return View();
        }

        //public ActionResult Agregar(string Nombre, DateTime Fecha)
        //{
        //    CategoriaBO ObjBO = new CategoriaBO();
        //    ObjBO.Nombre = Nombre;
        //    ObjBO.FechaCreacion = Fecha;
        //    ObjModel.Agregar(ObjBO);
        //    return RedirectToAction("Categoria");
        //}
    }
}