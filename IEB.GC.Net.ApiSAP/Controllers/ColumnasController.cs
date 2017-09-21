using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IEB.GC.Net.ApiSAP.Models;

namespace IEB.GC.Net.ApiSAP.Controllers
{
    public class ColumnasController : Controller
    {
        private APISAPEntities1 db = new APISAPEntities1();

        // GET: Elemento
        public ActionResult Index()
        {
            var columnas = db.Elementos.Include(c => c.SubestacionSAP);
            return View(columnas.ToList());
        }

        // GET: Elemento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elemento columna = db.Elementos.Find(id);
            if (columna == null)
            {
                return HttpNotFound();
            }
            return View(columna);
        }

        // GET: Elemento/Create
        public ActionResult Create()
        {
            Elemento modelo = new Elemento();
            modelo.Id_Subestacion = -1;
            return View(modelo);
        }

        // POST: Elemento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Elemento,TipoElemento,NumeroCuerpos,Id_Proyectos, NivelTension")] Elemento columna)
        {
            if (ModelState.IsValid)
            {
                db.Elementos.Add(columna);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Proyectos = new SelectList(db.SubestacionSAPs, "Id", "Revisor", columna.Id_Subestacion);
            return View(columna);
        }

        // GET: Elemento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elemento columna = db.Elementos.Find(id);
            if (columna == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Proyectos = new SelectList(db.SubestacionSAPs, "Id", "Revisor", columna.Id_Subestacion);
            return View(columna);
        }

        // POST: Elemento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Elemento,TipoElemento,NumeroCuerpos,Id_Proyectos, NivelTension")] Elemento columna)
        {
            if (ModelState.IsValid)
            {
                db.Entry(columna).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Proyectos = new SelectList(db.SubestacionSAPs, "Id", "Revisor", columna.Id_Subestacion);
            return View(columna);
        }

        // GET: Elemento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elemento columna = db.Elementos.Find(id);
            if (columna == null)
            {
                return HttpNotFound();
            }
            return View(columna);
        }

        // POST: Elemento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Elemento columna = db.Elementos.Find(id);
            db.Elementos.Remove(columna);
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
