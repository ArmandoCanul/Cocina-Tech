using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cocina_Tech.Models;
using Cocina_Tech.BO;


namespace Cocina_Tech.Controllers.Admin
{
    public class AdministradorController : Controller
    {
        PlatilloModel ObjModel = new PlatilloModel();
        CategoriaModel categoriamodel = new CategoriaModel();
        PlatilloBO platillobo = new PlatilloBO();

        // GET: Administrador
        Registro objusu = new Registro();
        MensajeModel objMen = new MensajeModel();
        public ActionResult Administrador()
        {
            ViewBag.Count = objMen.NumeroMensaje();
            return View();
        }

        public ActionResult Platillopingrediente()
        {
            return View(ObjModel.TABLA_PLATILLO().Tables[0]);
        }

        public ActionResult Platillo(int ID)
        {
            Session["IDPLATILLO"] = ID;
            return View(ObjModel.TABLA_PLATILLOID(ID).Tables[0]);
        }

        [ChildActionOnly]
        public ActionResult Ingredientexplatillo()
        {
            int ID = int.Parse(Session["IDPLATILLO"].ToString());
            return View(ObjModel.Tablaingred(ID));
        }

        [ChildActionOnly]
        public ActionResult Ingredientes()
        {
            return View(categoriamodel.Tabla_Ingrediente().Tables[0]);
        }

        [ChildActionOnly]
        public ActionResult Cantidad()
        {
            
            return View();
        }

        public ActionResult guardaring(List<int>ID,List<int>Cantidad)
        {

            for (int i = 0; i < Cantidad.Count; i++)
            {
                if (Cantidad[i]>0)
                {
                    platillobo.ID = int.Parse(Session["IDPLATILLO"].ToString());
                    platillobo.IDINGREDIENTE = ID[i];
                    platillobo.cantidad = Cantidad[i];
                    ObjModel.Agregarcantidad(platillobo);
                }
            }
           
            Platillo(platillobo.ID);
            return View("Platillo");

        }

        public ActionResult VotacionPlatillo()
        {

            return View(ObjModel.PlatilloTemporal().Tables[0]);
        }

        public ActionResult AgregarVotacion(string Nombre, string TiempoPreparacion, HttpPostedFileBase Portada, string IDC, string Precio, string Descrip)
        {
            VotacionBo ObjBO = new VotacionBo();
            CategoriaBO objC = new CategoriaBO();
            Galeria obJGa = new Galeria();
            ObjBO.Nombre = Nombre;

            ObjBO.FechaCreacion = DateTime.Now;

            ObjBO.Preparacion = TiempoPreparacion;


            ObjBO.Estado = false;
            if (Portada != null && Portada.ContentLength > 0)
            {
                string imgfile = Portada.FileName;
                ObjBO.Portada = new byte[Portada.ContentLength];
                Portada.InputStream.Read(ObjBO.Portada, 0, Portada.ContentLength);
            }
            ObjBO.Precio = Precio;
            ObjBO.Descripcion = Descrip;
            ObjBO.IDCategoria = int.Parse(IDC);

            ObjBO.IDusuario = int.Parse(Session["Usuario"].ToString());
            ObjModel.AgregarPlatilloTem(ObjBO);

            return RedirectToAction("VotacionPlatillo");
        }

        public ActionResult CambiarEstatus(int id)
        {
            VotacionBo objbo = new VotacionBo();
            objbo.Estado = true;
            objbo.ID = id;
            ObjModel.ActualizarEstatus(objbo);
            return RedirectToAction("VotacionPlatillo");
        }


        public ActionResult VotacionActualizar(string Nombre, string TiempoPreparacion, HttpPostedFileBase Portada, string IDC, string Precio, string Descrip)
        {
            VotacionBo ObjBO = new VotacionBo();
            CategoriaBO objC = new CategoriaBO();
            Galeria obJGa = new Galeria();
            ObjBO.Nombre = Nombre;

            ObjBO.FechaCreacion = DateTime.Now;

            ObjBO.Preparacion = TiempoPreparacion;


            ObjBO.Estado = false;
            if (Portada != null && Portada.ContentLength > 0)
            {
                string imgfile = Portada.FileName;
                ObjBO.Portada = new byte[Portada.ContentLength];
                Portada.InputStream.Read(ObjBO.Portada, 0, Portada.ContentLength);
            }
            ObjBO.Precio = Precio;
            ObjBO.Descripcion = Descrip;
            ObjBO.IDCategoria = int.Parse(IDC);

            ObjBO.IDusuario = int.Parse(Session["Usuario"].ToString());
            ObjModel.ActualizarVotacion(ObjBO);

            return RedirectToAction("VotacionPlatillo");
        }
    }
}