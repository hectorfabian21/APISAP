using IEB.GC.Net.ApiSAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IEB.GC.Net.ApiSAP.Controllers
{
    public class CasosController : Controller
    {
        APISAPEntities1 entidadesBD = new APISAPEntities1();

        // GET: Casos
        public ActionResult Index(int sid)
        {
            ViewData["subestacion_id"] = sid;
            List<Caso> casos = entidadesBD.Casos.ToList().FindAll(x => x.id_subestacion == sid);            
            return View(casos);
        }

        // GET: Casos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Casos/Create
        public ActionResult Create(int sid)
        {
            Caso caso = new Caso();
            caso.id_subestacion = sid;
            return View(caso);
        }

        // POST: Casos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Caso model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entidadesBD.Casos.Add(model);
                    entidadesBD.SaveChanges();

                }
                // TODO: Add insert logic here

                return RedirectToAction("Edit", new {id=model.Id });
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error", "Error en el formulario, intentelo nuevamente");
                return View();
            }
        }

        // GET: Casos/Edit/5
        public ActionResult Edit(int id)
        {
            ActionResult resultado=null;            
            Caso caso = entidadesBD.Casos.First(x => x.Id == id);

            if (caso != null)
            {
                resultado = View(caso);
            }                                       
            
            return resultado;
        }

        // POST: Casos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Caso modelo)
        {            
            try
            {
                if (ModelState.IsValid)
                {
                    Caso buscar = entidadesBD.Casos.First(x => x.Id == modelo.Id);
                    buscar.descripcion_caso = modelo.descripcion_caso;
                    buscar.id_subestacion = modelo.id_subestacion;
                    buscar.nombre_caso = modelo.nombre_caso;
                    entidadesBD.SaveChanges();                    
                }
                else
                {
                    ModelState.AddModelError("Error", "Error en los datos de ingreso");
                }
               
            }
            catch
            {
                ModelState.AddModelError("Error", "Error en los datos de ingreso");
            }
            return View();
        }

        // GET: Casos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Casos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
