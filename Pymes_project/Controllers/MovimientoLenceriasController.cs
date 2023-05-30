using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pymes_project.Models;
using Pymes_sitio_web.Data;

namespace Pymes_project.Controllers
{
    public class MovimientoLenceriasController : Controller
    {
        private Pymes_projectContext db = new Pymes_projectContext();

        // GET: MovimientoLencerias
        [Authorize(Roles = "View")]
        public ActionResult Index()
        {
            var movimientoLencerias = db.MovimientoLencerias.Include(m => m.InventarioLenceria);
            return View(movimientoLencerias.ToList());
        }

        // GET: MovimientoLencerias/Details/5
        [Authorize(Roles = "Details")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientoLenceria movimientoLenceria = db.MovimientoLencerias.Find(id);
            if (movimientoLenceria == null)
            {
                return HttpNotFound();
            }
            return View(movimientoLenceria);
        }

        // GET: MovimientoLencerias/Create
        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            ViewBag.idcodigo = new SelectList(db.InventarioLencerias, "idcodigo", "nombreproducto");
            return View();
        }

        // POST: MovimientoLencerias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idventa,idnropedido,idcodigo,cantidadvendida,valorventa")] MovimientoLenceria movimientoLenceria)
        {
            if (ModelState.IsValid)
            {
                db.MovimientoLencerias.Add(movimientoLenceria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idcodigo = new SelectList(db.InventarioLencerias, "idcodigo", "nombreproducto", movimientoLenceria.idcodigo);
            return View(movimientoLenceria);
        }

        // GET: MovimientoLencerias/Edit/5
        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientoLenceria movimientoLenceria = db.MovimientoLencerias.Find(id);
            if (movimientoLenceria == null)
            {
                return HttpNotFound();
            }
            ViewBag.idcodigo = new SelectList(db.InventarioLencerias, "idcodigo", "nombreproducto", movimientoLenceria.idcodigo);
            return View(movimientoLenceria);
        }

        // POST: MovimientoLencerias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idventa,idnropedido,idcodigo,cantidadvendida,valorventa")] MovimientoLenceria movimientoLenceria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimientoLenceria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idcodigo = new SelectList(db.InventarioLencerias, "idcodigo", "nombreproducto", movimientoLenceria.idcodigo);
            return View(movimientoLenceria);
        }

        // GET: MovimientoLencerias/Delete/5
        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientoLenceria movimientoLenceria = db.MovimientoLencerias.Find(id);
            if (movimientoLenceria == null)
            {
                return HttpNotFound();
            }
            return View(movimientoLenceria);
        }

        // POST: MovimientoLencerias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovimientoLenceria movimientoLenceria = db.MovimientoLencerias.Find(id);
            db.MovimientoLencerias.Remove(movimientoLenceria);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
