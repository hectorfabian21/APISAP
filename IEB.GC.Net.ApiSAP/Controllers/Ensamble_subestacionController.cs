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
    public class Ensamble_subestacionController : Controller
    {
        private APISAPEntities1 db = new APISAPEntities1();

        // GET: Ensamble_subestacion
        public ActionResult Index(int id)
        {            
            List<Ensamble_subestacion> ensamble = db.Ensamble_subestacions.ToList().FindAll(x=> x.id_Caso==id);
            return View(ensamble);
        }

        // GET: Ensamble_subestacion/Create
        public ActionResult Create()
        {
            DefaultListElementos();
            Ensamble_subestacion subensamble = new Ensamble_subestacion();
            subensamble.id_Subestacion = int.Parse(TempData.Peek("id").ToString());
            subensamble.id_Caso= int.Parse(TempData.Peek("id_caso").ToString());
            return View(subensamble);
        }

        // POST: Ensamble_subestacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ensamble_subestacion ensamble_subestacion)
        {
            DefaultListElementos();
            Elemento elem = db.Elementos.ToList().Find(x => (x.Id_Subestacion == ensamble_subestacion.id_Subestacion && x.Id_Elemento.Equals(ensamble_subestacion.Elemento)));
            ensamble_subestacion.Tipo_de_elemento = elem.TipoElemento;
            ensamble_subestacion.Id_Elemento = elem.Id;
            if(ensamble_subestacion.Elemento.StartsWith("V") || ensamble_subestacion.Elemento.StartsWith("v"))
            {                
            }
            else
            {
                ensamble_subestacion.Orientacion_viga = "";
            }

            if (ModelState.IsValid)
            {
                db.Ensamble_subestacions.Add(ensamble_subestacion);
                db.SaveChanges();
                return View(ensamble_subestacion);
            }
            return View(ensamble_subestacion);
        }

        // GET: Ensamble_subestacion/Edit/5
        public ActionResult Edit(int? id)
        {
            
            Ensamble_subestacion ensamble_subestacion = db.Ensamble_subestacions.Find(id);
            @ViewData["elemento"] = ensamble_subestacion.Elemento;
            DefaultListElementos();
            if (ensamble_subestacion == null)
            {
                return HttpNotFound();
            }           
            return View(ensamble_subestacion);
        }

        // POST: Ensamble_subestacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ensamble_subestacion ensamble_subestacion)
        {            
            Elemento elem = db.Elementos.ToList().Find(x => (x.Id_Subestacion == ensamble_subestacion.id_Subestacion && x.Id_Elemento.Equals(ensamble_subestacion.Elemento)));
            ensamble_subestacion.Tipo_de_elemento = elem.TipoElemento;
            ensamble_subestacion.Id_Elemento = elem.Id;
            if (ensamble_subestacion.Elemento.StartsWith("V") || ensamble_subestacion.Elemento.StartsWith("v"))
            {
            }
            else
            {
                ensamble_subestacion.Orientacion_viga = "";
            }

            if (ModelState.IsValid)
            {
                db.Entry(ensamble_subestacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = int.Parse(TempData.Peek("id_caso").ToString()) });
            }           
            return View(ensamble_subestacion);
        }

        // GET: Ensamble_subestacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ensamble_subestacion ensamble_subestacion = db.Ensamble_subestacions.Find(id);
            List<Carga> cargas = db.Cargas.ToList().FindAll(x => x.Id_Ensamble == ensamble_subestacion.Id);
            foreach (Carga item in cargas)
            {
                db.Cargas.Remove(item);
                db.SaveChanges();
            }

            db.Ensamble_subestacions.Remove(ensamble_subestacion);
            db.SaveChanges();
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

        #region ListElementos
        private void DefaultListElementos()
        {
            var Nombreselementos = db.Elementos.ToList().FindAll(x => x.Caso== int.Parse(TempData.Peek("id_caso").ToString())).Select(u => u.Id_Elemento);
            ViewBag.ListaElementos = new SelectList(Nombreselementos);
        }
        #endregion
    }
}
