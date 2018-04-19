using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cocina_Tech.BO;
using Cocina_Tech.Models;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Cocina_Tech.Controllers
{
    public class ReportesController : Controller
    {
        ConexionModel Con = new ConexionModel();
        ReportesModel ObjDAO = new ReportesModel();
         
        // GET: Reportes
        public ActionResult ReporteUsuario()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reportes/Viewers/"), "ReporteUsuarios.rpt"));

            rd.SetDataSource(ObjDAO.Usuarios());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "UsuariosReporte.pdf");        
        }     
        
        public ActionResult ReporteChef()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reportes/Viewers/"), "ReporteChef.rpt"));

            rd.SetDataSource(ObjDAO.UsuarioChef());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ChefReporte.pdf");
        } 
        
        public ActionResult ReporteIngrediente()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reportes/Viewers/"), "ReporteIngrerdientes.rpt"));

            rd.SetDataSource(ObjDAO.Ingredientes());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "IngredienteReporte.pdf");
        }      
    }
}