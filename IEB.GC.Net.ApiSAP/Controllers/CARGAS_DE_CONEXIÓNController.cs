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
    public class CARGAS_DE_CONEXIÓNController : Controller
    {
        private APISAPEntities1 db = new APISAPEntities1();

        // GET: CARGAS_DE_CONEXIÓN
        public ActionResult Index(int id)
        {
           
            List<CARGAS_DE_CONEXIÓN> cargas = new List<CARGAS_DE_CONEXIÓN>();
            cargas = db.CARGAS_DE_CONEXIÓN.ToList().FindAll(x => x.Id_Caso== id);            
            return View(cargas);
        }
        
        // GET: CARGAS_DE_CONEXIÓN/Create
        public ActionResult Create()
        {
            
            CARGAS_DE_CONEXIÓN carga = new CARGAS_DE_CONEXIÓN();
            carga.Id_Subestacion = int.Parse(TempData.Peek("id").ToString());
            carga.Id_Caso = int.Parse(TempData.Peek("id_caso").ToString());
            return View(carga);
        }

        // POST: CARGAS_DE_CONEXIÓN/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( CARGAS_DE_CONEXIÓN Carga)
        {
            var a = db.CARGAS_DE_CONEXIÓN.ToList().FindAll(x => (x.Id_Subestacion == Carga.Id_Subestacion && x.Templa_o_guarda.StartsWith("T")));
            var b = db.CARGAS_DE_CONEXIÓN.ToList().FindAll(x => (x.Id_Subestacion == Carga.Id_Subestacion && x.Templa_o_guarda.StartsWith("LL")));
            var c = db.CARGAS_DE_CONEXIÓN.ToList().FindAll(x => (x.Id_Subestacion == Carga.Id_Subestacion && x.Templa_o_guarda.StartsWith("G")));
            var d = db.CARGAS_DE_CONEXIÓN.ToList().FindAll(x => (x.Id_Subestacion == Carga.Id_Subestacion && x.Templa_o_guarda.StartsWith("GLL")));
            if (Carga.Templa_o_guarda.Equals("Templa"))
            {
                Carga.Templa_o_guarda = "T" + (a.Count+1);
            }
            else if(Carga.Templa_o_guarda.Equals("Templa de llegada"))
            {
                Carga.Templa_o_guarda = "LL" + (b.Count+1);
            }
            else if (Carga.Templa_o_guarda.Equals("Guarda"))
            {
                Carga.Templa_o_guarda = "G" + (c.Count-d.Count+1);
            }
            else if (Carga.Templa_o_guarda.Equals("Guarda de llegada"))
            {
                Carga.Templa_o_guarda = "GLL" + (d.Count+1);
            }

            if (ModelState.IsValid)
            {
                db.CARGAS_DE_CONEXIÓN.Add(Carga);
                db.SaveChanges();
                return View(Carga);
            }

            ViewBag.Id_Subestacion = new SelectList(db.SubestacionSAPs, "Id", "Revisor", Carga.Id_Subestacion);
            return View(Carga);
        }

        // GET: CARGAS_DE_CONEXIÓN/Edit/5
        public ActionResult Edit(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CARGAS_DE_CONEXIÓN Carga = db.CARGAS_DE_CONEXIÓN.Find(id);
            if (Carga == null)
            {
                return HttpNotFound();
            }           
            return View(Carga);
        }

        // POST: CARGAS_DE_CONEXIÓN/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( CARGAS_DE_CONEXIÓN cARGAS_DE_CONEXIÓN)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(cARGAS_DE_CONEXIÓN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = int.Parse(TempData.Peek("id_caso").ToString()) });
            }
            
            return View(cARGAS_DE_CONEXIÓN);
        }

        // GET: CARGAS_DE_CONEXIÓN/Delete/5
        public ActionResult Delete(int? id)
        {
            CARGAS_DE_CONEXIÓN car = db.CARGAS_DE_CONEXIÓN.Find(id);
            db.CARGAS_DE_CONEXIÓN.Remove(car);
            db.SaveChanges();
            //new { IdElemento = int.Parse(TempData.Peek("IdElemento").ToString()) }
            return RedirectToAction("Index", new { id = int.Parse(TempData.Peek("id_caso").ToString()) });
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
