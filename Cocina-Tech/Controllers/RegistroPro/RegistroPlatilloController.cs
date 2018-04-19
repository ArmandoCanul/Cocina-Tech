using Cocina_Tech.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cocina_Tech.Models;
using System.Data;
using System.IO;

namespace Cocina_Tech.Controllers.RegistroPro
{
    public class RegistroPlatilloController : Controller
    {
        PlatilloModel ObjModel = new PlatilloModel();

        CategoriaModel Objcat = new CategoriaModel();
        Registro objUsu = new Registro();
        // GET: RegistroPlatillo
        public ActionResult RegistroPlatillo()
        {
            return View(ObjModel.TABLA_PLATILLO().Tables[0]);
        }
        public ActionResult RegistroUsuarios()
        {
            return View(objUsu.Tabla_Usuario().Tables[0]);
        }


        public ActionResult EliminarUsu(string ID)
        {
            try
            {
                UsuarioBO objBO = new UsuarioBO();
                objBO.ID = int.Parse(ID);
                objBO.estado = false;
                objUsu.EliminarUsuario(objBO);
                RegistroUsuarios();
               
            }
            catch { }
            return View("RegistroUsuarios");
        }


        public ActionResult WebAdministrador()
        {
            return View();
        }
        public ActionResult Pedidos()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AgregarPlatillo(HttpPostedFileBase algo, string Nombre, string TiempoPreparacion, string IDC, HttpPostedFileBase Portada, string Precio /*HttpPostedFileBase[] Imagen*/)
        {
            PlatilloBO ObjBO = new PlatilloBO();
            CategoriaBO objC = new CategoriaBO();
            Galeria obJGa = new Galeria();
            ObjBO.Nombre = Nombre;


            if (algo != null && algo.ContentLength > 0)
            {
                string namefile = algo.FileName;
                ObjBO.Recetario = new byte[algo.ContentLength];
                algo.InputStream.Read(ObjBO.Recetario, 0, algo.ContentLength);
            }
            ObjBO.TiempoPreparacion = TiempoPreparacion;
            ObjBO.FechaCreacion = DateTime.Now;
            ObjBO.IDC = int.Parse(IDC);
            ObjBO.Estado = true;
            if (Portada != null && Portada.ContentLength > 0)
            {
                string imgfile = Portada.FileName;
                ObjBO.Portada = new byte[Portada.ContentLength];
                Portada.InputStream.Read(ObjBO.Portada, 0, Portada.ContentLength);
            }
            ObjBO.Precio = Precio;

            ObjBO.IDUSUARIO = int.Parse(Session["Usuario"].ToString());
            //if (Imagen != null && Imagen.Length > 0)
            //{
            //    for (int i = 0; i < Imagen.Length; i++)
            //    {
            //        string imgfile = Imagen[i].FileName;
            //        obJGa.Imagen = new byte[Imagen[i].ContentLength];
            //        Imagen[i].InputStream.Read(obJGa.Imagen, 0, Imagen[i].ContentLength);
            //    }

            //}
            //obJGa.Estado = true;
            //obJGa.Fecha = DateTime.Now;
            //obJGa.Id_platillo =
            ObjModel.AgregarPlatillo(ObjBO);
            //ObjModel.AgregarGaleria(obJGa);


            return RedirectToAction("RegistroPlatillo");
        }     


       
        [ChildActionOnly]
        public ActionResult ParcialPlatillo()
        {
            var cat = new PlatilloBO();
            cat.Categorias = ObjModel.listaCat();
            return PartialView("ParcialPlatillo", cat);
        }

        //[ChildActionOnly]
        public ActionResult MostrarPlatilloParcial()
        {
            return PartialView(ObjModel.Recuperar());
        }

        public ActionResult MostrarPlatillo()
        {
            return View(ObjModel.Recuperar());
        }
        
        public ActionResult RegistarCategorias()
        {
            return View(Objcat.Tabla_Categoria_bd());
        }

        public ActionResult Agregar(string Nombre)
        {
            CategoriaBO ObjBO = new CategoriaBO();
            ObjBO.Nombre = Nombre;
            ObjBO.FechaCreacion = DateTime.Now;
            ObjBO.Estado = true;
            Objcat.Agregar(ObjBO);
            RegistarCategorias();
            return RedirectToAction("RegistarCategorias");
        }

        public ActionResult Eliminarcat(string ID)
        {
            CategoriaBO objBO = new CategoriaBO();
            objBO.IDCT = int.Parse(ID);
            objBO.Estado = false;
            Objcat.EliminarCat(objBO);
            RegistarCategorias();
            return View("RegistarCategorias");
        }

     


        public ActionResult IngredienteM()
        {
            return View(Objcat.Tabla_Medida().Tables[0]);
        }


        public ActionResult AgregarMedida(string Medida)//ingresa medida
        {
            IngredienteM ObjBO = new BO.IngredienteM();
            ObjBO.Medida = Medida;
            ObjBO.EstadoM =true ;
            Objcat.AgregarMedida(ObjBO);
            return RedirectToAction("IngredienteM");
        }

        
        public ActionResult VistaIngrediente()
        {
            return View(Objcat.Tabla_Ingrediente().Tables[0]);
        }

        public ActionResult Ingrediente(string Nombre, string IDMedida)//Ingresar ingrediente
        {
            IngredientePrin OBJBO = new BO.IngredientePrin();
            IngredienteM ing = new BO.IngredienteM();
            OBJBO.Nombre = Nombre;
            OBJBO.EstadoI = true;
            OBJBO.Feha = DateTime.Now;
            OBJBO.idMedida = int.Parse(IDMedida);
            Objcat.AgregarIngrediente(OBJBO);
            return RedirectToAction("VistaIngrediente");
        }

        public ActionResult EliminarIng(string ID)
        {
           IngredientePrin  objBO = new IngredientePrin();
            objBO.IDIn = int.Parse(ID);
            objBO.EstadoI = false;
            Objcat.EliminarIng(objBO);
            RegistarCategorias();
            return View("RegistarCategorias");
        }


        [ChildActionOnly]
        public ActionResult vistaMedida()
        {
            var medida = new IngredientePrin();
            medida.Medidas = Objcat.listaMedida();
            return PartialView("vistaMedida", medida);
        }


        public ActionResult AgregarChef()
        {
            return View(objUsu.Tabla_Chef().Tables[0]); 
        }

        public ActionResult chefEmpleado(string Nombre, string Apellido, string Telefono, DateTime FechaNac, string Correo, string Usuario, string Contraseña, HttpPostedFileBase Fotografia)
        {
            UsuarioBO ObjBO = new UsuarioBO();
            ObjBO.Nombre = Nombre;
            ObjBO.Apellido = Apellido;
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
            objUsu.AgregarChef(ObjBO);

            return RedirectToAction("AgregarChef");

        }

        public ActionResult EliminarChef(string ID)
        {
            try
            {
                UsuarioBO objBO = new UsuarioBO();
                objBO.ID = int.Parse(ID);
                objBO.estado = false;
                objUsu.EliminarChef(objBO);
                AgregarChef();

            }
            catch { }
            return View("AgregarChef");
        }
        [ChildActionOnly]
        public ActionResult Agregaringredentespatillo ()
        {

            return PartialView();
        }

    }
}