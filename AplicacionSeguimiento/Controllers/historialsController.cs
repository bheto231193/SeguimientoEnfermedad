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
    public class historialsController : Controller
    {
        private SeguimientoEnfermedadEntities db = new SeguimientoEnfermedadEntities();

        // GET: historials
        public ActionResult Index()
        {
            var historial = db.historial.Include(h => h.Persona);
            return View(historial.ToList());
        }

        // GET: historials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historial historial = db.historial.Find(id);
            if (historial == null)
            {
                return HttpNotFound();
            }
            return View(historial);
        }

        // GET: historials/Create
        public ActionResult Create()
        {
            ViewBag.idpaciente = new SelectList(db.Persona, "id", "nombre");
            return View();
        }

        // POST: historials/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idhistorial,diagnosticoPasado,diagnosticoConsulta,idpaciente")] historial historial)
        {
            if (ModelState.IsValid)
            {
                db.historial.Add(historial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idpaciente = new SelectList(db.Persona, "id", "nombre", historial.idpaciente);
            return View(historial);
        }

        // GET: historials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historial historial = db.historial.Find(id);
            if (historial == null)
            {
                return HttpNotFound();
            }
            ViewBag.idpaciente = new SelectList(db.Persona, "id", "nombre", historial.idpaciente);
            return View(historial);
        }

        // POST: historials/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idhistorial,diagnosticoPasado,diagnosticoConsulta,idpaciente")] historial historial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idpaciente = new SelectList(db.Persona, "id", "nombre", historial.idpaciente);
            return View(historial);
        }

        // GET: historials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            historial historial = db.historial.Find(id);
            if (historial == null)
            {
                return HttpNotFound();
            }
            return View(historial);
        }

        // POST: historials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            historial historial = db.historial.Find(id);
            db.historial.Remove(historial);
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
