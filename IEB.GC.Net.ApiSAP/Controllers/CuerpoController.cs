using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IEB.GC.Net.ApiSAP.Models;
using System.Data.Entity;

namespace IEB.GC.Net.ApiSAP.Controllers
{
    public class CuerpoController : Controller
    {
        private APISAPEntities1 db = new APISAPEntities1();

        public static Repositorio repositorio = new Repositorio();
       
        // GET: Cuerpo
        public ActionResult Index(int IdElemento)
        {
            TempData["IdElemento"] = IdElemento;
            TempData.Keep("IdElemento");
            Elemento elem = db.Elementos.Find(IdElemento);
            TempData["NumeroCuerpo"] = elem.NumeroCuerpos;
            TempData.Keep("NumeroCuerpo");
            List<CuerposDeElemento> lista = db.CuerposDeElementoes.ToList().FindAll(x=>x.Id_Elemento==IdElemento);
            return View(lista);
        }

        public ActionResult Crear()
        {
            DefaultListPerfiles();
            int numeroCuerpo = db.CuerposDeElementoes.ToList().FindAll(x => x.Id_Elemento == int.Parse(TempData.Peek("IdElemento").ToString())).Count;
            TempData["cuerpos"] = numeroCuerpo;
            TempData["guardado"] = "";
            CuerposDeElemento nuevoCuerpo = new CuerposDeElemento();
            nuevoCuerpo.Id_Elemento = int.Parse(TempData.Peek("IdElemento").ToString());
            Elemento elem = db.Elementos.Find(nuevoCuerpo.Id_Elemento);
            nuevoCuerpo.Nivel_de_tension = elem.NivelTension;
            return View(nuevoCuerpo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(CuerposDeElemento modelo) {
            if (ModelState.IsValid)
            {
                DefaultListPerfiles();
                Elemento elem = db.Elementos.Find(modelo.Id_Elemento);
                modelo.Nivel_de_tension = elem.NivelTension;
                db.CuerposDeElementoes.Add(modelo);
                db.SaveChanges();

                int numeroCuerpo = db.CuerposDeElementoes.ToList().FindAll(x => x.Id_Elemento == modelo.Id_Elemento).Count;
                TempData["guardado"] = "Registro guardado con exito";
                TempData["cuerpos"] = numeroCuerpo;
                return View(modelo);                
            }
            else
            {
                ModelState.AddModelError("error", "error en agregar el elemento");
                return View(modelo);
            }

        }

        public ActionResult Editar(int id)
        {
            DefaultListPerfiles();
            CuerposDeElemento oModel = new CuerposDeElemento();
            oModel = db.CuerposDeElementoes.Find(id);
            return View(oModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(CuerposDeElemento modelo)
        {           
                        
            db.Entry(modelo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Cuerpo", new { IdElemento = int.Parse(TempData.Peek("IdElemento").ToString()) });
        }


        public ActionResult Borrar (int id)
        {
            CuerposDeElemento cuerpo = db.CuerposDeElementoes.Find(id);
            db.CuerposDeElementoes.Remove(cuerpo);
            db.SaveChanges();
            return RedirectToAction("Index", "Cuerpo", new { IdElemento = int.Parse(TempData.Peek("IdElemento").ToString()) });
        }

       


        #region ListPerfiles
        private void DefaultListPerfiles()
        {           
            var NombresPerfil = db.Perfiles.ToList().FindAll(x =>( x.id_subestacion== int.Parse(TempData.Peek("id").ToString()))).Select(u => u.Nombre_del_perfil);
            ViewBag.ListaPerfiles = new SelectList(NombresPerfil);
        }
        #endregion
    }
}