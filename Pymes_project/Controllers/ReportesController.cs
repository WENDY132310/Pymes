using Microsoft.Reporting.WebForms;
using Pymes_project.Models;
using Pymes_sitio_web.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pymes_project.Controllers
{
    public class ReportesController : Controller
    {
        private Pymes_projectContext db = new Pymes_projectContext();
        // GET: Reportes
        public ActionResult ReporteClientes()
        {
            return View(db.Clientes.ToList());
        }
        public ActionResult VistaReporteClientes(string id)
        {
            LocalReport reporte = new LocalReport();
            string ruta = Path.Combine(Server.MapPath("~/Informes"), "ReporteClientes.rdlc");
            reporte.ReportPath = ruta;
            List<Clientes> listado = new List<Clientes>();
            listado = db.Clientes.ToList();
            ReportDataSource verreporte = new ReportDataSource("Reporte_de_Clientes", listado);
            reporte.DataSources.Add(verreporte);
            string tiporeporte = id;
            string mime, codificacion, archivo;
            string[] flujo;
            Warning[] aviso;
            string dispositivo = @"<DeviceInfo>" +
            " <OutputFormat>" + id + "</OutputFormat>" +
            " <PageWidth>8.5in</PageWidth>" +
            " <PageHeight>11in</PageHeight>" +
            " <MarginTop>0.5in</MarginTop>" +
            " <MarginLeft>1in</MarginLeft>" +
            " <MarginRight>1in</MarginRight>" +
            " <MarginBottom>0.5in</MarginBottom>" +
            " <EmbedFonts>None</EmbedFonts>" +
            "</DeviceInfo>";
            byte[] enviar = reporte.Render(id, dispositivo, out mime,
           out codificacion, out archivo, out flujo, out aviso);
            return File(enviar, mime);
        }
    }
}