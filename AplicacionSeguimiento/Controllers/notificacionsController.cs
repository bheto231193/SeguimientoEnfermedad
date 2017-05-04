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
    public class notificacionsController : Controller
    {
        private SeguimientoEnfermedadEntities db = new SeguimientoEnfermedadEntities();

        // GET: notificacions
        public ActionResult Index()
        {
            var notificacion = db.notificacion.Include(n => n.agenda);
            return View(notificacion.ToList());
        }

        // GET: notificacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            notificacion notificacion = db.notificacion.Find(id);
            if (notificacion == null)
            {
                return HttpNotFound();
            }
            return View(notificacion);
        }

        // GET: notificacions/Create
        public ActionResult Create()
        {
            ViewBag.idagenda = new SelectList(db.agenda, "idagenda", "fechaNotificacion");
            return View();
        }

        // POST: notificacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idnotificacion,fechaMed,proximasCitas,idagenda")] notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                db.notificacion.Add(notificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idagenda = new SelectList(db.agenda, "idagenda", "fechaNotificacion", notificacion.idagenda);
            return View(notificacion);
        }

        // GET: notificacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            notificacion notificacion = db.notificacion.Find(id);
            if (notificacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idagenda = new SelectList(db.agenda, "idagenda", "fechaNotificacion", notificacion.idagenda);
            return View(notificacion);
        }

        // POST: notificacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idnotificacion,fechaMed,proximasCitas,idagenda")] notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idagenda = new SelectList(db.agenda, "idagenda", "fechaNotificacion", notificacion.idagenda);
            return View(notificacion);
        }

        // GET: notificacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            notificacion notificacion = db.notificacion.Find(id);
            if (notificacion == null)
            {
                return HttpNotFound();
            }
            return View(notificacion);
        }

        // POST: notificacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            notificacion notificacion = db.notificacion.Find(id);
            db.notificacion.Remove(notificacion);
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
