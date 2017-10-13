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
    public class Cargas_AutomaticaController : Controller
    {
        private APISAPEntities1 db = new APISAPEntities1();

        // GET: Cargas_Automatica
        public ActionResult Index(int id_subestacion)
        {
                       
            List<Cargas_Automatica> automatica = db.Cargas_Automaticas.ToList().FindAll(x => x.Id_subestacion == id_subestacion);
            if(automatica.Count==0)
            {
                Cargas_Automatica car = new Cargas_Automatica();
                car.Id_subestacion = id_subestacion;
                car.Tipo_exposicion = "C";
                car.K_exponente = 1;
                car.Velocidad_viento = 100;
                car.Factor_importancia = 1;
                car.Factor_topografia = 1;
                car.Factor_rafaga = 1;
                car.Factor_direccion = 0.85;
                car.R_Porticos = 3;
                car.R_Equipos = 1;
                db.Cargas_Automaticas.Add(car);
                db.SaveChanges();
                automatica = db.Cargas_Automaticas.ToList().FindAll(x => x.Id_subestacion == id_subestacion);
            }
            
            return View(automatica);
        }
        

        // GET: Cargas_Automatica/Edit/5
        public ActionResult Edit(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargas_Automatica cargas_Automatica = db.Cargas_Automaticas.Find(id);
            if (cargas_Automatica == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_caso = new SelectList(db.Casos, "Id", "nombre_caso", cargas_Automatica.id_caso);
            ViewBag.Id_subestacion = new SelectList(db.SubestacionSAPs, "Id", "Revisor", cargas_Automatica.Id_subestacion);
            return View(cargas_Automatica);
        }

        // POST: Cargas_Automatica/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Cargas_Automatica cargas_Automatica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargas_Automatica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new { id_subestacion = int.Parse(TempData.Peek("id").ToString()) });
            }
            ViewBag.id_caso = new SelectList(db.Casos, "Id", "nombre_caso", cargas_Automatica.id_caso);
            ViewBag.Id_subestacion = new SelectList(db.SubestacionSAPs, "Id", "Revisor", cargas_Automatica.Id_subestacion);
            return View(cargas_Automatica);
        }
               
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Vistas parciales

        #region tipos de carga
        public ActionResult ListarTipos()
        {
                        
            List<Tipos_de_carga> tipos = db.Tipos_de_cargas.ToList().FindAll(x=>(x.id_subestacion==0 || x.id_subestacion == int.Parse(TempData.Peek("id").ToString())));

            return PartialView(tipos);

        }

        public ActionResult agregartipodecarga()
        {
            Tipos_de_carga tipo = new Tipos_de_carga();
            tipo.id_subestacion = int.Parse(TempData.Peek("id").ToString());
            return View(tipo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult agregartipodecarga(Tipos_de_carga tipo)
        {

            if (ModelState.IsValid)
            {
                db.Tipos_de_cargas.Add(tipo);
                db.SaveChanges();
                return RedirectToAction("Index",new { id_subestacion = int.Parse(TempData.Peek("id").ToString())});
            }


            return View(tipo);
        }

        public ActionResult editartipodecarga(int id)
        {
            Tipos_de_carga carga = db.Tipos_de_cargas.Find(id);
            return View(carga);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editartipodecarga(Tipos_de_carga carga)
        {

            if (ModelState.IsValid)
            {
                db.Entry(carga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id_subestacion = int.Parse(TempData.Peek("id").ToString()) });
            }
            return View(carga);
        }

        public ActionResult borrartipodecarga(int id)
        {
            Tipos_de_carga tipo = db.Tipos_de_cargas.Find(id);
            db.Tipos_de_cargas.Remove(tipo);
            db.SaveChanges();
            return RedirectToAction("Index", new { id_subestacion = int.Parse(TempData.Peek("id").ToString()) });
        }
        #endregion

        #region Combinaciones
        public ActionResult Listarcombinaciones()
        {            
            List<Combinacione> combinaciones = db.Combinaciones.ToList().FindAll(x => (x.id_subestacion == 0 || x.id_subestacion == int.Parse(TempData.Peek("id").ToString())));

            foreach(Combinacione comb in combinaciones)
            {
                List<Casos_de_Combinacione> casos = db.Casos_de_Combinaciones.ToList().FindAll(x => x.Combinacion.Equals(comb.Nombre));
                string descripcion = "";
                foreach (Casos_de_Combinacione caso in casos)
                {
                    if(caso.Factor_de_modificacion>0)
                    {
                        descripcion = descripcion+" +"+caso.Factor_de_modificacion+"*"+caso.Tipo;
                    }
                    else
                    {
                        descripcion = descripcion+" " + caso.Factor_de_modificacion + "*" + caso.Tipo;
                    }
                    
                }
                comb.Descripcion = descripcion;
                db.Entry(comb).State = EntityState.Modified;
                db.SaveChanges();


            }
            combinaciones = db.Combinaciones.ToList().FindAll(x => (x.id_subestacion == 0 || x.id_subestacion == int.Parse(TempData.Peek("id").ToString())));

            return PartialView(combinaciones);

        }
        public ActionResult agregarcombinacion()
        {
           
            Combinacione comb = new Combinacione();
            comb.id_subestacion = int.Parse(TempData.Peek("id").ToString());
            
            return View(comb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult agregarcombinacion(Combinacione combo)
        {

            if (ModelState.IsValid)
            {
                db.Combinaciones.Add(combo);
                db.SaveChanges();
                return RedirectToAction("Index", new { id_subestacion = int.Parse(TempData.Peek("id").ToString()) });
            }
                       
            return View(combo);
        }

        public ActionResult agregarcaso(int id)
        {
            Listtipos();
            Combinacione comb = db.Combinaciones.ToList().Find(x => x.Id == id);
            Casos_de_Combinacione caso = new Casos_de_Combinacione();
            caso.Caso = string.Empty;
            caso.Tipo = string.Empty;
            caso.Combinacion = comb.Nombre;
            caso.id_subestacion = comb.id_subestacion;
            return View(caso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult agregarcaso(Casos_de_Combinacione ModeloCaso)
        {
            Listtipos();
            if (ModelState.IsValid)
            {
                db.Casos_de_Combinaciones.Add(ModeloCaso);
                db.SaveChanges();
                return View(ModeloCaso);               
            }

            return View(ModeloCaso);
        }

       public ActionResult editarcombinacion(int id)
        {
            ViewData["combinacion_id"] = id;
            TempData["comb"] = id;
            TempData.Keep("comb");
            Combinacione comb = db.Combinaciones.Find(id);
            List<Casos_de_Combinacione> lista = db.Casos_de_Combinaciones.ToList().FindAll(x => x.Combinacion.Equals(comb.Nombre));
            
            return View(lista);
        }
        
        public ActionResult borrarcombinacion(int id)
        {
            Combinacione combinacion = db.Combinaciones.Find(id);
            List<Casos_de_Combinacione> casoscomb = db.Casos_de_Combinaciones.ToList().FindAll(x => x.Combinacion.Equals(combinacion.Nombre));
            foreach(Casos_de_Combinacione item in casoscomb)
            {
                db.Casos_de_Combinaciones.Remove(item);
                db.SaveChanges();
            }
            db.Combinaciones.Remove(combinacion);
            db.SaveChanges();
            return RedirectToAction("Index", new { id_subestacion = int.Parse(TempData.Peek("id").ToString()) });
        }

        public ActionResult editarcaso(int id)
        {
            Listtipos();
            Casos_de_Combinacione tipo = db.Casos_de_Combinaciones.Find(id);          

            return View(tipo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editarcaso(Casos_de_Combinacione tipocombinacion)
        {
            Listtipos();
            if (ModelState.IsValid)
            {
                db.Entry(tipocombinacion).State = EntityState.Modified;
                db.SaveChanges();                
               return RedirectToAction("editarcombinacion", new { id = int.Parse(TempData.Peek("comb").ToString()) });
            }

            return View(tipocombinacion);
        }

        public ActionResult borrarcaso(int id)
        {
            Casos_de_Combinacione tipo = db.Casos_de_Combinaciones.Find(id);
            db.Casos_de_Combinaciones.Remove(tipo);
            db.SaveChanges();
            return RedirectToAction("editarcombinacion", new { id = int.Parse(TempData.Peek("comb").ToString()) });
        }

        #endregion
        #endregion

        private void Listtipos()
        {
            var NombresTipo = db.Tipos_de_cargas.ToList().FindAll(x=>(x.id_subestacion==0|| x.id_subestacion == int.Parse(TempData.Peek("id").ToString()))).Select(u => u.Convencion);
            ViewBag.Listatipos = new SelectList(NombresTipo);
        }

    }
}
