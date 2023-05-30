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
    public class InventarioLenceriasController : Controller
    {
        private Pymes_projectContext db = new Pymes_projectContext();

        // GET: InventarioLencerias
        [Authorize(Roles = "View")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.InventarioLencerias.ToList());
        }

        // GET: InventarioLencerias/Details/5
        
        [Authorize(Roles = "Details")]
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioLenceria inventarioLenceria = db.InventarioLencerias.Find(id);
            if (inventarioLenceria == null)
            {
                return HttpNotFound();
            }
            return View(inventarioLenceria);
        }

        // GET: InventarioLencerias/Create
        [Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: InventarioLencerias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idcodigo,nombreproducto,cantidad,valorunitario")] InventarioLenceria inventarioLenceria)
        {
            if (ModelState.IsValid)
            {
                db.InventarioLencerias.Add(inventarioLenceria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inventarioLenceria);
        }

        // GET: InventarioLencerias/Edit/5
        [Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioLenceria inventarioLenceria = db.InventarioLencerias.Find(id);
            if (inventarioLenceria == null)
            {
                return HttpNotFound();
            }
            return View(inventarioLenceria);
        }

        // POST: InventarioLencerias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idcodigo,nombreproducto,cantidad,valorunitario")] InventarioLenceria inventarioLenceria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventarioLenceria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventarioLenceria);
        }

        // GET: InventarioLencerias/Delete/5
        [Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventarioLenceria inventarioLenceria = db.InventarioLencerias.Find(id);
            if (inventarioLenceria == null)
            {
                return HttpNotFound();
            }
            return View(inventarioLenceria);
        }

        // POST: InventarioLencerias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InventarioLenceria inventarioLenceria = db.InventarioLencerias.Find(id);
            db.InventarioLencerias.Remove(inventarioLenceria);
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
