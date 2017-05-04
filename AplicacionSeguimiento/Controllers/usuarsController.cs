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
    public class usuarsController : Controller
    {
        private SeguimientoEnfermedadEntities db = new SeguimientoEnfermedadEntities();

        // GET: usuars
        public ActionResult Index()
        {
            var usuar = db.usuar.Include(u => u.Persona).Include(u => u.TipoUsuario);
            return View(usuar.ToList());
        }

        // GET: usuars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuar usuar = db.usuar.Find(id);
            if (usuar == null)
            {
                return HttpNotFound();
            }
            return View(usuar);
        }

        // GET: usuars/Create
        public ActionResult Create()
        {
            ViewBag.idPersona = new SelectList(db.Persona, "id", "nombre");
            ViewBag.idTipoUsuario = new SelectList(db.TipoUsuario, "idTipoUsuario", "tipoUsuario1");
            return View();
        }

        // POST: usuars/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUsuario,usuario,pass,idTipoUsuario,idPersona")] usuar usuar)
        {
            if (ModelState.IsValid)
            {
                db.usuar.Add(usuar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPersona = new SelectList(db.Persona, "id", "nombre", usuar.idPersona);
            ViewBag.idTipoUsuario = new SelectList(db.TipoUsuario, "idTipoUsuario", "tipoUsuario1", usuar.idTipoUsuario);
            return View(usuar);
        }

        // GET: usuars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuar usuar = db.usuar.Find(id);
            if (usuar == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPersona = new SelectList(db.Persona, "id", "nombre", usuar.idPersona);
            ViewBag.idTipoUsuario = new SelectList(db.TipoUsuario, "idTipoUsuario", "tipoUsuario1", usuar.idTipoUsuario);
            return View(usuar);
        }

        // POST: usuars/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUsuario,usuario,pass,idTipoUsuario,idPersona")] usuar usuar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPersona = new SelectList(db.Persona, "id", "nombre", usuar.idPersona);
            ViewBag.idTipoUsuario = new SelectList(db.TipoUsuario, "idTipoUsuario", "tipoUsuario1", usuar.idTipoUsuario);
            return View(usuar);
        }

        // GET: usuars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuar usuar = db.usuar.Find(id);
            if (usuar == null)
            {
                return HttpNotFound();
            }
            return View(usuar);
        }

        // POST: usuars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuar usuar = db.usuar.Find(id);
            db.usuar.Remove(usuar);
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
