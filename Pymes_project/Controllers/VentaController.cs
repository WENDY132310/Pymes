using Pymes_sitio_web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pymes_project.ModeloVistas;
using Pymes_project.Models;
using System.Net;

namespace Pymes_project.Controllers
{
    public class VentaController : Controller
    {
        
        private Pymes_projectContext bd = new Pymes_projectContext();
        // GET: Venta
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult NuevoPedido()
        {
            ModeloVistas.Pedido nuevopedido = new ModeloVistas.Pedido();
            nuevopedido.Clientes = new ModeloVistas.Clientes();
            nuevopedido.Productos = new List<ModeloVistas.InventarioLenceria>();
            Session["Pedido"] = nuevopedido;
            var lista = bd.Clientes.ToList();
            lista.Add(new Models.Clientes { idnit = 0, empresa = "[Seleccione una empresa...]" });
            lista = lista.OrderBy(e => e.empresa).ToList();
            ViewBag.idnit = new SelectList(lista, "idnit", "empresa");
            return View(nuevopedido);

        }
        
        // Procedimiento para adicionar un nuevo pedido
        [HttpPost]
        //recibe un parámetro nuevopedido de tipo VistaPedido
        public ActionResult NuevoPedido(ModeloVistas.Pedido vistapedido)
        {
            vistapedido = Session["Pedido"] as ModeloVistas.Pedido;
            int idnit = int.Parse(Request["idnit"]);
            int nropedido = int.Parse(Request["Clientes.nropedido"]);
            DateTime fechapedido = Convert.ToDateTime(Request["Clientes.fechaventa"]);
            string comenta = Request["Clientes.comentario"];
            // Se valida que se tenga un numero de pedido y que se haya seleccionado un cliente
            if (idnit == 0 || nropedido == 0)
            {
                ViewBag.Error = "Sin número de pedido y/o Cliente no seleccionado...";
                var lista = bd.Clientes.ToList();
                lista.Add(new Models.Clientes { idnit = 0, empresa = "[Seleccione una empresa...]" });
                lista = lista.OrderBy(e => e.empresa).ToList();
                ViewBag.idnit = new SelectList(lista, "idnit", "empresa");
                return View(vistapedido);
            }
            else
            {
                int ultimonropedido = 0;
                // se obtiene el idnit del cliente.
                var clientes = bd.Clientes.FirstOrDefault(x => x.idnit == idnit);
                int nit = clientes.nit;
                // se inicia la transacción que se guardara en la base de datos
                using (var transaccion = bd.Database.BeginTransaction())
                {
                    try
                    {
                        // Inicia el proceso para obtener y guardar los datos del pedido //
                        Venta nuevo = new Models.Venta
                        {
                            nropedido = nropedido,
                            idnit = idnit,
                            fechaventa = fechapedido,
                            comentario = comenta
                        };
                        bd.Ventas.Add(nuevo);
                        bd.SaveChanges();
                        // finaliza proceso para obtener y guardar los datos del pedido //
                        // Inicia el proceso para obtener y guardar los productos del pedido //
                        ultimonropedido = bd.Ventas.ToList().Select(o => o.idnropedido).Max();
                        foreach (ModeloVistas.InventarioLenceria elemento in vistapedido.Productos)
                        {
                            var compras = new MovimientoLenceria()
                            {
                                idnropedido = ultimonropedido,
                                idcodigo = elemento.idcodigo,
                                cantidadvendida = elemento.cantidadvendida,
                                valorventa = elemento.valorunitario*elemento.cantidadvendida
                            };
                            bd.MovimientoLencerias.Add(compras);
                        }
                        bd.SaveChanges();

                        foreach (ModeloVistas.InventarioLenceria elemento in vistapedido.Productos)
                        {
                            var compras = new Compras()
                            {
                                idnropedido = ultimonropedido,
                                idcodigo = elemento.idcodigo,
                                cantidadvendida = elemento.cantidadvendida,
                                valorventa = elemento.valorunitario * elemento.cantidadvendida
                            };
                            bd.Compras.Add(compras);
                        }
                        bd.SaveChanges();
                        // finaliza proceso para obtener y guardar los productos del pedido //
                        // Inicia el proceso para modificar la cantidad de productos //
                        foreach (ModeloVistas.InventarioLenceria elemento in vistapedido.Productos)
                        {
                            var productos = bd.InventarioLencerias.FirstOrDefault(x => x.idcodigo ==
                           elemento.idcodigo);
                            int vr = productos.cantidad;
                            vr = vr - elemento.cantidadvendida;
                            productos.cantidad = vr;
                            bd.InventarioLencerias.Attach(productos);
                            bd.Entry(productos).Property(x => x.cantidad).IsModified = true;
                            bd.SaveChanges();
                        }
                        // Finaliza el proceso para modificar la cantidad de productos //
                        transaccion.Commit();
                        // se finaliza la transacción que se guardara en la base de datos
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        ViewBag.Error = "Error:" + ex.Message;
                        var listacl = bd.Clientes.ToList();
                        listacl.Add(new Models.Clientes
                        {
                            idnit = 0,
                            empresa = "[Seleccione una empresa...]" });
                    listacl = listacl.OrderBy(e => e.empresa).ToList();
                        ViewBag.idnit = new SelectList(listacl, "idnit", "empresa");
                        return View(vistapedido);
                    }
                }
                vistapedido = Session["Pedido"] as ModeloVistas.Pedido;
                ViewBag.Mensaje = string.Format("El pedido: {0}, se guardo exitosamente....",
               nropedido);
                var lista = bd.Clientes.ToList();
                lista.Add(new Models.Clientes
                {
                    idnit = 0,
                    empresa = "[Seleccione una empresa...]" });
            lista = lista.OrderBy(e => e.empresa).ToList();
                ViewBag.idnit = new SelectList(lista, "idnit", "empresa");
                vistapedido.Productos.Clear();
                return View(vistapedido);
            }
        }
        // Procedimiento para seleccionar un producto
        
        [HttpGet]
        public ActionResult AdicionarProducto()
        {
            var listap = bd.InventarioLencerias.ToList();
            listap.Add(new Models.InventarioLenceria
            {
                idcodigo = 0,
                nombreproducto = "[Seleccione un producto...]" });
        listap = listap.OrderBy(p => p.nombreproducto).ToList();
            ViewBag.idcodigo = new SelectList(listap, "idcodigo", "nombreproducto");
            return View();
        }

        // Procedimiento para adicionar productos
        [HttpPost]
        public ActionResult AdicionarProducto(ModeloVistas.InventarioLenceria productospedido)
        {
            var nuevopedido = Session["Pedido"] as ModeloVistas.Pedido;
            var idcodigo = int.Parse(Request["idcodigo"]);
            // Se valida que se tenga un producto seleccionado
            if (idcodigo == 0)
            {
                ViewBag.Error = "Producto no seleccionado...";
                var listap = bd.InventarioLencerias .ToList();
                listap.Add(new Models.InventarioLenceria
                {
                    idcodigo = 0,
                    nombreproducto = "[Seleccione un producto...]" });
            listap = listap.OrderBy(p => p.nombreproducto).ToList();
                ViewBag.idcodigo = new SelectList(listap, "idcodigo", "nombreproducto");
                return View();
            }
            else
            {
                var productov = bd.InventarioLencerias.Find(idcodigo);
                int micant = int.Parse(Request["cantidadvendida"]);
                // Se valida que la cantidad digita sea menor que la cantidad existente
                if (productov.cantidad < micant)
                {
                    ViewBag.Error = "Cantidad en existencia es menor que la cantida vendida...";
                    var listap = bd.InventarioLencerias.ToList();
                    listap.Add(new Models.InventarioLenceria
                    {
                        idcodigo = 0,
                        nombreproducto = "[Seleccione un producto...]" });
                listap = listap.OrderBy(p => p.nombreproducto).ToList();
                    ViewBag.idcodigo = new SelectList(listap, "idcodigo", "nombreproducto");
                    return View();
                }
                else
                {
                    // Inicia el proceso para adicionar el producto a la vista //
                    productospedido = new ModeloVistas.InventarioLenceria()
                    {
                        idcodigo = productov.idcodigo,
                        codigo = productov.codigo,
                        nombreproducto = productov.nombreproducto,
                        valorunitario = productov.valorunitario,
                        cantidadvendida = int.Parse(Request["cantidadvendida"])
                    };
                    nuevopedido.Productos.Add(productospedido);
                    // Finaliza el proceso para adicionar el producto a la vista //
                    var lista = bd.Clientes.ToList();
                    lista.Add(new Models.Clientes
                    {
                        idnit = 0,
                        empresa = "[Seleccione una empresa...]" });
                lista = lista.OrderBy(e => e.empresa).ToList();
                    ViewBag.idnit = new SelectList(lista, "idnit", "empresa");
                    var listap = bd.InventarioLencerias.ToList();
                    listap.Add(new Models.InventarioLenceria
                    {
                        idcodigo = 0,
                        nombreproducto = "[Seleccione un producto...]" });
                listap = listap.OrderBy(p => p.nombreproducto).ToList();
                    ViewBag.idcodigo = new SelectList(listap, "idcodigo", "nombreproducto");
                    return View("NuevoPedido", nuevopedido);
                }
            }
        }
        // Se libera memoria //
        protected override void Dispose(bool liberar)
        {
            if (liberar)
                bd.Dispose();
            base.Dispose(liberar);
        }
    }
}

