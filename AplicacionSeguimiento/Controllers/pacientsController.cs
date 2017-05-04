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
    public class pacientsController : Controller
    {
        private SeguimientoEnfermedadEntities db = new SeguimientoEnfermedadEntities();

        // GET: pacients
        public ActionResult Index()
        {
            var pacient = db.pacient.Include(p => p.agenda).Include(p => p.Persona);
            return View(pacient.ToList());
        }

        // GET: pacients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pacient pacient = db.pacient.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }

        // GET: pacients/Create
        public ActionResult Create()
        {
            ViewBag.idagenda = new SelectList(db.agenda, "idagenda", "fechaNotificacion");
            ViewBag.id = new SelectList(db.Persona, "id", "nombre");
            return View();
        }

        // POST: pacients/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idpaciente,padecimiento,idagenda,id")] pacient pacient)
        {
            if (ModelState.IsValid)
            {
                db.pacient.Add(pacient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idagenda = new SelectList(db.agenda, "idagenda", "fechaNotificacion", pacient.idagenda);
            ViewBag.id = new SelectList(db.Persona, "id", "nombre", pacient.id);
            return View(pacient);
        }

        // GET: pacients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pacient pacient = db.pacient.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            ViewBag.idagenda = new SelectList(db.agenda, "idagenda", "fechaNotificacion", pacient.idagenda);
            ViewBag.id = new SelectList(db.Persona, "id", "nombre", pacient.id);
            return View(pacient);
        }

        // POST: pacients/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idpaciente,padecimiento,idagenda,id")] pacient pacient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pacient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idagenda = new SelectList(db.agenda, "idagenda", "fechaNotificacion", pacient.idagenda);
            ViewBag.id = new SelectList(db.Persona, "id", "nombre", pacient.id);
            return View(pacient);
        }

        // GET: pacients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pacient pacient = db.pacient.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }

        // POST: pacients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pacient pacient = db.pacient.Find(id);
            db.pacient.Remove(pacient);
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
