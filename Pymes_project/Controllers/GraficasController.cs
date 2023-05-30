using Pymes_project.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pymes_project.Controllers
{
    public class GraficasController : Controller
    {
        BD_PymesEntitiesProductos grafica = new BD_PymesEntitiesProductos();
        public ActionResult Grafica()
        {
            return View();
        }
        public ActionResult VistaGrafica()
        {
            ArrayList xvalores = new ArrayList();
            ArrayList yvalores = new ArrayList();
            var resultados = grafica.InventarioLencerias.ToList();
            resultados.ToList().ForEach(rs => xvalores.Add(rs.nombreproducto));
            resultados.ToList().ForEach(rs => yvalores.Add(rs.cantidad));
            var grafico = new System.Web.Helpers.Chart(width: 500, height: 600)
            .AddTitle("Ventas")
            .AddSeries(chartType: "Pie", name: "Barras", xValue: xvalores, yValues:
           yvalores);
            return File(grafico.GetBytes("png"), "Image/png");
        }

    }
}