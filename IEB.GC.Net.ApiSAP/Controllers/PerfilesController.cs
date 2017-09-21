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
    public class PerfilesController : Controller
    {
        private APISAPEntities1 db = new APISAPEntities1();
        public static Repositorio Repositori = new Repositorio();
        // GET: Perfiles
        public ActionResult Index(int id)
        {
            
            List<Perfile> perfileria = db.Perfiles.ToList().FindAll(x => x.id_subestacion== int.Parse(TempData.Peek("id").ToString()));
            return View(perfileria);
        }
               

        // GET: Perfiles/Create
        public ActionResult Create(int sid)
        {
            
            DefaultListMateriales();
            Perfile perfil = new Perfile();
            perfil.id_subestacion = sid;
            return View(perfil);
        }

        // POST: Perfiles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Perfile perfile)
        {
            perfile.t2__mm_ = perfile.t3___mm_;
            perfile.tw__mm_ = perfile.tf__mm_;
            perfile.Uso_del_perfil = "";
            perfile.Tipo_del_perfil = "Angle";
            if (ModelState.IsValid)
            {
                db.Perfiles.Add(perfile);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = int.Parse(TempData.Peek("id").ToString()) });
            }
            DefaultListMateriales();
            return View(perfile);
        }

        // GET: Perfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            DefaultListMateriales();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfile perfile = db.Perfiles.Find(id);
            if (perfile == null)
            {
                return HttpNotFound();
            }
            return View(perfile);
        }

        // POST: Perfiles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Perfile perfile)
        {
            DefaultListMateriales();
            perfile.t2__mm_ = perfile.t3___mm_;
            perfile.tw__mm_ = perfile.tf__mm_;
            perfile.Uso_del_perfil = "";
            perfile.Tipo_del_perfil = "Angle";
            if (ModelState.IsValid)
            {
                Repositori.agregarPerfil(perfile);                
                return RedirectToAction("Index", new { id = int.Parse(TempData.Peek("id").ToString()) });
            }
            return View(perfile);
        }

        // GET: Perfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            Perfile perfile = db.Perfiles.Find(id);
            db.Perfiles.Remove(perfile);
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
        private void DefaultListMateriales()
        {
            var NombresMateriales = db.Materials.ToList().Select(u => u.NombreMaterial);
            ViewBag.ListaMateriales = new SelectList(NombresMateriales);
        }
    }

}
