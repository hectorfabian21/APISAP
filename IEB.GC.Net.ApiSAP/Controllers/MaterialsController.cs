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
    public class MaterialsController : Controller
    {
        private APISAPEntities1 db = new APISAPEntities1();

        // GET: Materials
        public ActionResult Index(int id)
        {
           
            List<Material> materiales = new List<Material>();
            materiales = db.Materials.ToList();
            return View(materiales);
        }

        
        // GET: Materials/Create
        public ActionResult Create()
        {
            Material defaultmaterial = new Material();
            defaultmaterial = db.Materials.ToList().Find(x => x.Id == 1);
            
            return View(defaultmaterial);
        }

        // POST: Materials/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Material material)
        {
            material.TipoMaterial = "Acero";
            if (ModelState.IsValid)
            {
                db.Materials.Add(material);
                db.SaveChanges();               
                return RedirectToAction("Index",new {id= int.Parse(TempData.Peek("id").ToString()) });
            }

            return View(material);
        }

        // GET: Materials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }

        // POST: Materials/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Material material)
        {
            material.TipoMaterial = "Acero";
            if (ModelState.IsValid)
            {
                db.Entry(material).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = int.Parse(TempData.Peek("id").ToString()) });
            }
            return View(material);
        }

        // GET: Materials/Delete/5
        public ActionResult Delete(int? id)
        {
            Material material = db.Materials.Find(id);
            db.Materials.Remove(material);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = int.Parse(TempData.Peek("id").ToString()) });
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
