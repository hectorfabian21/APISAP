using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IEB.GC.Net.ApiSAP.Models;

namespace IEB.GC.Net.ApiSAP.Controllers
{
    public class DefinicionCargasController : Controller
    {
        private APISAPEntities1 db = new APISAPEntities1();
        public static Repositorio Repositori = new Repositorio();
        // GET: DefinicionCargas
        public ActionResult Index(int id_subestacion,int id_caso)
        {
            Cargas_Automatica carga = new Cargas_Automatica();
            carga.Id_subestacion = id_subestacion;
            int defecto = 0;
            List<Tipos_de_carga> lista = db.Tipos_de_cargas.ToList();

            Tipos_de_carga tipos = lista.Find(x=>x.id_subestacion==id_subestacion);



            return View();
        }
    }
}