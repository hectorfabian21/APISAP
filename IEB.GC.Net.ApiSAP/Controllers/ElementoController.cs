using IEB.GC.Net.ApiSAP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IEB.GC.Net.ApiSAP.Controllers
{
    public class ElementoController : Controller
    {
        private APISAPEntities1 db = new APISAPEntities1();

        // GET: Elemento
        public ActionResult Index(int sid)
        {
            ViewData["id_caso"] = sid;
            TempData["id_caso"] = sid;
            TempData.Keep("id_caso");
            
            List<Elemento> modelo = db.Elementos.ToList().FindAll(x => x.Caso == sid); 
            
            return View(modelo);
        }
        //new { id = int.Parse(TempData.Peek("sid").ToString()) }
        // GET: Elemento/Create
        public ActionResult Create()
        {            
            Elemento modelo = new Elemento();
            modelo.Id_Subestacion = int.Parse(TempData.Peek("id").ToString());
            modelo.Caso= int.Parse(TempData.Peek("id_caso").ToString());
            return View(modelo);
        }

        // POST: Elemento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Elemento columna)
        {
            string Tipo = "", FirstName="";            
            if (ModelState.IsValid)
            {
                if (columna.TipoElemento == "Columna Portico") { Tipo = "Columna Portico"; FirstName = "C"; }
                else if (columna.TipoElemento == "Viga Portico") { Tipo = "Viga Portico"; FirstName = "V"; }
                else { Tipo = "Soporte Equipo"; FirstName = "S"; }
                var b = db.Elementos.ToList().FindAll(x => (x.Id_Subestacion == columna.Id_Subestacion && x.TipoElemento.Equals(Tipo))).Select(u => u.Id_Elemento);
                var a = b.Count() + 1;
                columna.Id_Elemento = FirstName + a.ToString();
                db.Elementos.Add(columna);
                db.SaveChanges();
                return View(columna);
            }                      
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
        public ActionResult Edit(Elemento columna)
        {
            string Tipo = "", FirstName = "";
            if (ModelState.IsValid)
            {
                Elemento actualizar = db.Elementos.Find(columna.Id); //te hace la referencia en la base de datos, del registro                    
                if (columna.TipoElemento == "Columna Portico") { Tipo = "Columna Portico"; FirstName = "C"; }
                else if (columna.TipoElemento == "Viga Portico") { Tipo = "Viga Portico"; FirstName = "V"; }
                else { Tipo = "Soporte Equipo"; FirstName = "S"; }
                var b = db.Elementos.ToList().FindAll(x => (x.Id_Subestacion == columna.Id_Subestacion && x.TipoElemento.Equals(Tipo))).Select(u => u.Id_Elemento);
                var a = b.Count() + 1;
                columna.Id_Elemento = FirstName + a.ToString();               
                actualizar.Id_Elemento = columna.Id_Elemento;
                actualizar.TipoElemento = columna.TipoElemento;
                actualizar.NumeroCuerpos = columna.NumeroCuerpos;
                actualizar.Id_Subestacion = columna.Id_Subestacion;
                actualizar.NivelTension = columna.NivelTension;                
                db.Entry(actualizar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new {sid= int.Parse(TempData.Peek("id_caso").ToString()) });
            }           
            return View(columna);
        }

        // GET: Elemento/Delete/5
        public ActionResult Delete(int? id)
        {
            List<CuerposDeElemento> List = db.CuerposDeElementoes.ToList().FindAll(x => x.Id_Elemento == id);
            foreach (CuerposDeElemento i in List)
            {
                db.CuerposDeElementoes.Remove(i);
                db.SaveChanges();
            }
            Elemento columna = db.Elementos.Find(id);
            int sid = columna.Caso;
            db.Elementos.Remove(columna);
            db.SaveChanges();
            return RedirectToAction("Index", new { sid = int.Parse(TempData.Peek("id_caso").ToString()) });
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
