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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        Registro osua = new Registro();
        MensajeModel objMen = new MensajeModel();
        public ActionResult UsuarioPrincipal()
        {
            if (Session["TipoUsuario"] == null)
            {
                return RedirectToAction("LoginP", "Login");
            }
            else if (Convert.ToInt32(Session["TipoUsuario"]) == 2)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LoginP", "Login");
            }


        }

        public ActionResult Miperfil()
        {
            ViewBag.Count = objMen.NumeroMensaje();
            int clave = Convert.ToInt32(Session["Usuario"]);
            return View(osua.RecuperaUsuario(clave).Tables[0]);
        }

        public ActionResult TarjetaCredit()
        {
            return View();
        }

        public ActionResult imboxUsuario()
        {
            ViewBag.Count = objMen.NumeroMensaje();
            int id = int.Parse(Session["Usuario"].ToString());
            return View(objMen.MensajesRecibidosUsuario(id).Tables[0]);
        }

        public ActionResult ResponderUsuario(int ID)
        {
            return View(objMen.vermensaje(ID).Tables[0]);
        }

        public ActionResult Responder(string Mensaje, string Asunto, string idenvia2)
        {
            int idenvia = Convert.ToInt32(Session["Usuario"]);
            MensajeModel mensobj = new MensajeModel();
            mensobj.Mensajeenviado(Mensaje, idenvia, Asunto, idenvia2);
            imboxUsuario();
            return View("imboxUsuario");
        }
        public ActionResult ConfigurarCuenta(string Nombre, string Apellidos, string Telefono, DateTime FechaNaci, string Correo, string Usuario, string Contraseña, HttpPostedFileBase Fotografia)
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
            ObjBO.ID = int.Parse(Session["Usuario"].ToString());
            osua.ActualizarUsuario(ObjBO);
            Miperfil();
            return RedirectToAction("MiPerfil");
        }

        public ActionResult MenuUsu()
        {
            PlatilloModel objModel = new PlatilloModel();
            return View(objModel.PlatilloPortada().Tables[0]);
        }

        public ActionResult Platillovotacion()
        {
            PlatilloModel objModel = new PlatilloModel();

            DataTable dt = objModel.VotoPlatillo(int.Parse(Session["Usuario"].ToString())).Tables[0];

            if (dt.Rows.Count > 0)
            {
                try
                {
                    ViewBag.platilo1 = dt.Rows[0]["ID_TEM"].ToString();
                    ViewBag.platilo2 = dt.Rows[1]["ID_TEM"].ToString();
                    ViewBag.platilo3 = dt.Rows[2]["ID_TEM"].ToString();
                }
                catch (Exception)
                {


                }

                return View(objModel.PlatilloVotacion().Tables[0]);

            }
            else
            {
                return View(objModel.PlatilloVotacion().Tables[0]);
            }
        }
        public ActionResult ActualizarUsu()
        {
            return View();

        }

        public ActionResult pruebamapa()
        {
            return View();
        }

        public ActionResult pruebamapa2()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Mapa()
        {
            return View();
        }

        public ActionResult Factura()
        {
            PedidoModel objPedido = new PedidoModel();
            var pedidos = objPedido.ObtenerPedido(int.Parse(Session["Usuario"].ToString()));

            List<PedidoBO> pedidoslist = new List<PedidoBO>();
            foreach (DataRow p in pedidos.Rows)
            {
                PedidoBO pedido = new PedidoBO();

                pedido.ESTADO = bool.Parse(p["ESTADO"].ToString());
                pedido.TOTAL = double.Parse(p["TOTAL"].ToString());
                pedido.FECHAPEDIDO = DateTime.Parse(p["FECHAPEDIDO"].ToString());
                pedido.HORAPEDIDO = TimeSpan.Parse(p["HORAPEDIDO"].ToString());
                pedido.HORAENTREGA = TimeSpan.Parse(p["HORAENTREGA"].ToString());
                pedido.ID = int.Parse(p["ID"].ToString());

                var detalles = objPedido.ObtenerDetallePedido(pedido.ID);

                pedido.DETALLES = new List<DetallePedidoBO>();
                foreach (DataRow dp in detalles.Rows)
                {
                    DetallePedidoBO detalle = new DetallePedidoBO();

                    detalle.ID = int.Parse(dp["ID"].ToString());
                    detalle.NOMBRE = dp["NOMBRE"].ToString();
                    detalle.PRECIO = dp["PRECIO"].ToString();

                    pedido.DETALLES.Add(detalle);
                }


                pedidoslist.Add(pedido);
            }

            return View(pedidoslist);
        }

        public ActionResult ListadoGeneral()
        {
            PedidoModel objPedido = new PedidoModel();
            var pedidos = objPedido.ObtenerPedidoGeneral();

            List<PedidoBO> pedidoslist = new List<PedidoBO>();
            foreach (DataRow p in pedidos.Rows)
            {
                PedidoBO pedido = new PedidoBO();

                pedido.Usuario = p["NOMBRE"].ToString();
                pedido.ESTADO = bool.Parse(p["ESTADO"].ToString());
                pedido.TOTAL = double.Parse(p["TOTAL"].ToString());
                pedido.FECHAPEDIDO = DateTime.Parse(p["FECHAPEDIDO"].ToString());
                pedido.HORAPEDIDO = TimeSpan.Parse(p["HORAPEDIDO"].ToString());
                pedido.HORAENTREGA = TimeSpan.Parse(p["HORAENTREGA"].ToString());
                pedido.ID = int.Parse(p["ID"].ToString());

                var detalles = objPedido.ObtenerDetallePedido(pedido.ID);

                pedido.DETALLES = new List<DetallePedidoBO>();
                foreach (DataRow dp in detalles.Rows)
                {
                    DetallePedidoBO detalle = new DetallePedidoBO();

                    detalle.ID = int.Parse(dp["ID"].ToString());
                    detalle.NOMBRE = dp["NOMBRE"].ToString();
                    detalle.PRECIO = dp["PRECIO"].ToString();

                    pedido.DETALLES.Add(detalle);
                }


                pedidoslist.Add(pedido);
            }
            return View(pedidoslist.ToList());
        }
    }
}