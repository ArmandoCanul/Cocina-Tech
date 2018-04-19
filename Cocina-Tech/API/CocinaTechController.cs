using Cocina_Tech.BO;
using Cocina_Tech.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Cocina_Tech.API
{
    public class UsuarioLogueado
    {
        public string Nombre { get; set; }
        public string ID { get; set; }
        public bool Logueado { get; set; }
    }

    public class Menu
    {
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public string Precio { get; set; }
    }

    [RoutePrefix("api/cocina")]
    public class CocinaTechController : ApiController
    {
        Registro ObjModel = new Registro();


        [HttpGet]
        [Route("login/{usuario}/{password}")]
        public HttpResponseMessage Login(string usuario, string password)
        {
            UsuarioBO ObjBO = new UsuarioBO();
            ObjBO.Usuario = usuario;
            ObjBO.Contraseña = ObjBO.Encriptar(password);

            var Tabla = ObjModel.Login(ObjBO);

            if(Tabla.Tables.Count > 0 && Tabla.Tables[0].Rows.Count > 0)
            {
                int id = Convert.ToInt32(Tabla.Tables[0].Rows[0]["ID"]);
                DataTable tabla = ObjModel.datos(id).Tables[0];

                if (tabla.Rows.Count > 0)
                {
                    UsuarioLogueado usuarioLogueado = new UsuarioLogueado()
                    {
                        ID = Tabla.Tables[0].Rows[0]["ID"].ToString(),
                        Nombre = tabla.Rows[0]["Usuario"].ToString(),
                        Logueado = true
                    };

                    var response = this.Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(usuarioLogueado), Encoding.UTF8, "application/json");

                    return response;
                }
            }
            else
            {
                UsuarioLogueado usuarioLogueado = new UsuarioLogueado()
                {
                    ID = "NO",
                    Nombre = "NO",
                    Logueado = false
                };

                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(usuarioLogueado), Encoding.UTF8, "application/json");

                return response;
            }

            var responsee = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            responsee.Content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");

            return responsee;
        }

        [HttpGet]
        [Route("menu/{usuario}")]
        public HttpResponseMessage GetMenu(string usuario)
        {
            PlatilloModel objModel = new PlatilloModel();

            var Tabla = objModel.PlatilloPortada().Tables[0];

            if (Tabla.Rows.Count > 0)
            {
                List<Menu> menu = new List<Menu>();

                foreach(DataRow dr in Tabla.Rows)
                {
                    Menu item = new Menu()
                    {
                        Nombre = dr["NOMBRE"].ToString(),
                        Precio = dr["PRECIO"].ToString(),
                        Imagen = ""
                    };

                    menu.Add(item);                    
                }

                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(menu), Encoding.UTF8, "application/json");

                return response;
            }

            var responsee = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            responsee.Content = new StringContent(JsonConvert.SerializeObject("NO ITEMS"), Encoding.UTF8, "application/json");

            return responsee;
        }

        [HttpGet]
        [Route("pedidos/{usuario}")]
        public HttpResponseMessage GetPedidos(string usuario)
        {
            PedidoModel objPedido = new PedidoModel();
            var pedidos = objPedido.ObtenerPedidoGeneral();

            List<PedidoBO> pedidoslist = new List<PedidoBO>();
            if (pedidos.Rows.Count > 0)
            {
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

                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(pedidoslist), Encoding.UTF8, "application/json");

                return response;

            }
            var responsee = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            responsee.Content = new StringContent(JsonConvert.SerializeObject("NO ITEMS"), Encoding.UTF8, "application/json");

            return responsee;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}