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
    public class CargasController : Controller
    {
        private APISAPEntities1 db = new APISAPEntities1();

        // GET: Cargas
        public ActionResult Index(int id_ensamble)
        {
            ViewData["id_ensamble"] = id_ensamble;
            TempData["id_em"] = id_ensamble;
            TempData.Keep("id_em");
           
            List<Carga> car = db.Cargas.ToList().FindAll(x => x.Id_Ensamble == id_ensamble);            
            return View(car);
        }
        // GET: Cargas/Create
        public ActionResult Create(int id_ensamble)
        {              
            DefaultListcargas();
            Carga car = new Carga();
            car.Id_Ensamble = id_ensamble;
            return View(car);
        }

        // POST: Cargas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Carga carga)
        {
            DefaultListcargas();
            Ensamble_subestacion ensamble = db.Ensamble_subestacions.Find(carga.Id_Ensamble);
            
            if (ModelState.IsValid)
            {
                CARGAS_DE_CONEXIÓN fuerza = db.CARGAS_DE_CONEXIÓN.ToList().Find(x => (x.Id_Caso== int.Parse(TempData.Peek("id_caso").ToString()) && x.Templa_o_guarda.Equals(carga.Templa_o_guarda)));
                carga.Id_Carga_de_conexion = fuerza.Id;
                if (fuerza.Templa_o_guarda.StartsWith("G"))
                {
                    carga.Numero_de_fases = 1;
                }
                else
                {
                    carga.Numero_de_fases = 3;
                }
                db.Cargas.Add(carga);
                db.SaveChanges();
                return View(carga);
            }
            
                                   
            return View(carga);
        }

        // GET: Cargas/Edit/5
        public ActionResult Edit(int? id)
        {
            DefaultListcargas();
            Carga carga = db.Cargas.Find(id);
            carga = db.Cargas.ToList().Find(x => x.Id == id);
            ViewData["separacion"] = carga.Separacion_diferentes;            
            return View(carga);
        }

        // POST: Cargas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Carga carga)
        {
            Ensamble_subestacion ensamble = db.Ensamble_subestacions.Find(carga.Id_Ensamble);
            if (ModelState.IsValid)
            {
                CARGAS_DE_CONEXIÓN fuerza = db.CARGAS_DE_CONEXIÓN.ToList().Find(x => (x.Id_Caso == int.Parse(TempData.Peek("id_caso").ToString()) && x.Templa_o_guarda.Equals(carga.Templa_o_guarda)));
                if (carga.Separacion_diferentes.Equals("No"))
                {
                    carga.Orden = 1;
                }
                carga.Id_Carga_de_conexion = fuerza.Id;
                if (fuerza.Templa_o_guarda.StartsWith("G"))
                {
                    carga.Numero_de_fases = 1;
                }
                else
                {
                    carga.Numero_de_fases = 3;
                }
                db.Entry(carga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id_ensamble = int.Parse(TempData.Peek("id_em").ToString()) });
            }            
            DefaultListcargas();            
            return View(carga);
        }

        // GET: Cargas/Delete/5
        public ActionResult Delete(int? id)
        {
            Carga carga = db.Cargas.Find(id);
            db.Cargas.Remove(carga);
            db.SaveChanges();
            return RedirectToAction("Index", new { id_ensamble = int.Parse(TempData.Peek("id_em").ToString()) });
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Listdecargas
        private void DefaultListcargas()
        {
            var Nombrescargas = db.CARGAS_DE_CONEXIÓN.ToList().FindAll(x=>x.Id_Caso== int.Parse(TempData.Peek("id_caso").ToString())).Select(u=>u.Templa_o_guarda);
            ViewBag.Listacargas = new SelectList(Nombrescargas);
        }
        #endregion
    }
}
