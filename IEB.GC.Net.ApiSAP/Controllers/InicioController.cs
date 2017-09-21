using IEB.GC.Net.ApiSAP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IEB.GC.Net.ApiSAP.Controllers
{
    public class InicioController : Controller
    {
        public static APISAPEntities1 entidadesBD = new APISAPEntities1();
        private APISAPEntities1 db = new APISAPEntities1();
        // GET: Inicio
        public ActionResult Index()
        {
            List<SubestacionSAP> subestacion = new List<SubestacionSAP>();
            subestacion = db.SubestacionSAPs.ToList();
            return View(subestacion);
        }

        public ActionResult Detalles(int id)
        {
            SubestacionSAP subestacion = db.SubestacionSAPs.Find(id);
            ViewData["subestacion_id"] = id;
            TempData["id"] = id;
            TempData.Keep("id");
            ViewData["id_caso"] = 0;
            TempData["id_caso"] = 0;
            TempData.Keep("id_caso");
            return View(subestacion);
        }

        public ActionResult Crear()
        {
            SubestacionSAP subestacionnueva = new SubestacionSAP();

            return View(subestacionnueva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(SubestacionSAP modelo)
        {
            modelo.Id_Subestacion = modelo.Id;
            ActionResult resultado;
            if (ModelState.IsValid)
            {
                entidadesBD.SubestacionSAPs.Add(modelo);
                entidadesBD.SaveChanges();
                SubestacionSAP subestacion = db.SubestacionSAPs.ToList().Find(x=>x.NombreProyecto.Equals(modelo.NombreProyecto));
                subestacion.Id_Subestacion = subestacion.Id;
                db.Entry(subestacion).State = EntityState.Modified;
                db.SaveChanges();
                string item= "F:"+ Path.DirectorySeparatorChar+ "Proyectos Aplicacion SAP2000"+ Path.DirectorySeparatorChar + modelo.NombreSubestacion;
                Directory.CreateDirectory(item);
                
                resultado = RedirectToAction("Index", "Inicio");
            }
            else
            {
                ModelState.AddModelError("Error","Error en los datos de ingreso");
                resultado = View();
            }

            return resultado;

        }
       
        public ActionResult editar(int id)
        {
            SubestacionSAP subestacion = db.SubestacionSAPs.Find(id);
            return View(subestacion);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editar(SubestacionSAP modelo)
        {
            modelo.Id_Subestacion = modelo.Id;
            if (ModelState.IsValid)
            {
                var consultarBD = from b in db.SubestacionSAPs
                                  where b.Id == modelo.Id
                                  select b.NombreSubestacion;

                string nombreAnterior="";
                foreach (var campos in consultarBD)
                {
                    nombreAnterior = campos;
                }

                db.Entry(modelo).State = EntityState.Modified;
                db.SaveChanges();
                string itemanterior = "F:" +Path.DirectorySeparatorChar + "Proyectos Aplicacion SAP2000" + Path.DirectorySeparatorChar + nombreAnterior;
                string item = "F:" + Path.DirectorySeparatorChar + "Proyectos Aplicacion SAP2000" + Path.DirectorySeparatorChar + modelo.NombreSubestacion;
                Directory.Move(itemanterior,item);
                


                return RedirectToAction("Index", "Inicio");
            }
            return View(modelo);           
        }
         #region vista parcial de casos 
        public ActionResult Casos()
        {
            List<Caso> modelos = db.Casos.ToList().FindAll(x => x.id_subestacion == int.Parse(TempData.Peek("id").ToString()));            
            return PartialView(modelos);
        }

        public ActionResult crearcaso(int id)
        {            
            Caso modelo = new Caso();
            modelo.id_subestacion = id;

            return View(modelo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult crearcaso(Caso modelo)
        {
            if (ModelState.IsValid)
            {
                db.Casos.Add(modelo);
                db.SaveChanges();
                SubestacionSAP subestacion = db.SubestacionSAPs.Find(modelo.id_subestacion);
                string item = "F:" + Path.DirectorySeparatorChar + "Proyectos Aplicacion SAP2000" + Path.DirectorySeparatorChar + subestacion.NombreSubestacion + Path.DirectorySeparatorChar + modelo.nombre_caso;
                Directory.CreateDirectory(item);
                return RedirectToAction("Detalles", "Inicio", new { id = int.Parse(TempData.Peek("id").ToString()) });
            }

            return View(modelo);

        }

        public ActionResult editarcaso(int id)
        {
            Caso modelo = db.Casos.Find(id);
            return View(modelo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editarcaso(Caso modelo)
        {
            if (ModelState.IsValid)
            {
                var consultarBD = from b in db.Casos
                                  where b.Id == modelo.Id
                                  select b.nombre_caso;
                string nombreAnterior = "";
                foreach (var campos in consultarBD)
                {
                    nombreAnterior = campos;
                }
                
                db.Entry(modelo).State = EntityState.Modified;                
                db.SaveChanges();
                SubestacionSAP subestacion = db.SubestacionSAPs.Find(modelo.id_subestacion);
                string itemanterior = "F:" + Path.DirectorySeparatorChar + "Proyectos Aplicacion SAP2000" + Path.DirectorySeparatorChar + subestacion.NombreSubestacion + Path.DirectorySeparatorChar + nombreAnterior;
                string item = "F:" + Path.DirectorySeparatorChar + "Proyectos Aplicacion SAP2000" + Path.DirectorySeparatorChar + subestacion.NombreSubestacion + Path.DirectorySeparatorChar + modelo.nombre_caso;
                Directory.Move(itemanterior, item);
                return RedirectToAction("Detalles", "Inicio", new { id = int.Parse(TempData.Peek("id").ToString()) });
            }

            return View(modelo);
        }

        public ActionResult ingresarcaso(int id)
        {
            ViewData["id_caso"] = id;
            TempData["id_caso"] = id;
            TempData.Keep("id_caso");
            Caso modelo = db.Casos.Find(id);
            SubestacionSAP subestacion = db.SubestacionSAPs.ToList().Find(x => x.Id == modelo.id_subestacion);
            Subestacioncaso model = new Subestacioncaso();
            model.id_subestacion = subestacion.Id;
            model.Id = modelo.Id;
            model.Revisor = subestacion.Revisor;
            model.Aprobador = subestacion.Aprobador;
            model.NombreProyecto = subestacion.NombreProyecto;
            model.NombreSubestacion = subestacion.NombreSubestacion;
            model.NombreModelo = subestacion.NombreModelo;
            model.nombre_caso = modelo.nombre_caso;
            model.descripcion_caso = modelo.descripcion_caso;
            return View(model);
        }

        #endregion
    }
}
