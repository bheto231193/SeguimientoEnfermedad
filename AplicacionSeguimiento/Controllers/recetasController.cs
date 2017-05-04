using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AplicacionSeguimiento;

namespace AplicacionSeguimiento.Controllers
{
    public class recetasController : Controller
    {
        private SeguimientoEnfermedadEntities db = new SeguimientoEnfermedadEntities();

        // GET: recetas
        public ActionResult Index()
        {
            var receta = db.receta.Include(r => r.medico);
            return View(receta.ToList());
        }

        // GET: recetas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receta receta = db.receta.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            return View(receta);
        }

        // GET: recetas/Create
        public ActionResult Create()
        {
            ViewBag.idmedico = new SelectList(db.medico, "idmedico", "cedula");
            return View();
        }

        // POST: recetas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idreceta,fecha,prescripcion,idmedico,vigencia")] receta receta)
        {
            if (ModelState.IsValid)
            {
                db.receta.Add(receta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idmedico = new SelectList(db.medico, "idmedico", "cedula", receta.idmedico);
            return View(receta);
        }

        // GET: recetas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receta receta = db.receta.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            ViewBag.idmedico = new SelectList(db.medico, "idmedico", "cedula", receta.idmedico);
            return View(receta);
        }

        // POST: recetas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idreceta,fecha,prescripcion,idmedico,vigencia")] receta receta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idmedico = new SelectList(db.medico, "idmedico", "cedula", receta.idmedico);
            return View(receta);
        }

        // GET: recetas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receta receta = db.receta.Find(id);
            if (receta == null)
            {
                return HttpNotFound();
            }
            return View(receta);
        }

        // POST: recetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            receta receta = db.receta.Find(id);
            db.receta.Remove(receta);
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
