using IEB.GC.Net.ApiSAP.Models;
using SAP2000v19;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IEB.GC.Net.ApiSAP.Controllers
{
    public class CargaSapController : Controller
    {
        public static Repositorio Repositori = new Repositorio();
        public static ElemtosSAP subes = new ElemtosSAP();
        APISAPEntities1 entidadesBD = new APISAPEntities1();
        // GET: CargaSap
        public ActionResult Index()
        {
            return View(subes);
        }

        public ActionResult abrirsap(int id_caso, int id)
        {
            ElemtosSAP nuevo = Repositori.abrir_sap2000();
            subes.Modelo = nuevo.Modelo;
            subes.Objeto = nuevo.Objeto;
            cSapModel subestacion = subes.Modelo;//INICIALIZAR EL SAP
            int ret = 0;
            Repositori.Materiales(subestacion);
            Repositori.tiposdecarga(subestacion, id);
            Repositori.Mass_Source(subestacion, id);
            Repositori.combinaciones(subestacion, id);
            Repositori.perfilesdefault(subestacion, id_caso);
            Repositori.perfilesagregados(subestacion, id);
            Repositori.diseño(subestacion);
            #region grupos generales 
            ret = subestacion.GroupDef.SetGroup("Montante");
            ret = subestacion.GroupDef.SetGroup("Diagonal");
            ret = subestacion.GroupDef.SetGroup("Montante Viga");
            ret = subestacion.GroupDef.SetGroup("Diagonal viga");
            ret = subestacion.GroupDef.SetGroup("Montante Castillete");
            ret = subestacion.GroupDef.SetGroup("Diagonal castillete");
            ret = subestacion.GroupDef.SetGroup("Cierres");
            ret = subestacion.GroupDef.SetGroup("Apoyos");
            #endregion

            List<Ensamble_subestacion> lista = entidadesBD.Ensamble_subestacions.AsNoTracking().ToList().FindAll(x => x.id_Caso == id_caso);
            foreach (Ensamble_subestacion item in lista)
            {
                if (item.Elemento.ToUpper().StartsWith("C"))
                {
                    Repositori.crear_columna(item, subestacion);//Crea columnas
                }
                else if (item.Elemento.ToUpper().StartsWith("V"))
                {
                    Repositori.crear_viga(item, subestacion);//Crea columnas
                }
                else if (item.Elemento.ToUpper().StartsWith("E"))
                {

                }

            }

            //habilitar guardar
            subes.estadocorrerproyecto = true;
            //habilitar salir
            subes.estadosalir = true;

            return RedirectToAction("Index", "CargaSap");

        }
        public ActionResult Guardarsap()
        {
            int id = int.Parse(TempData.Peek("id").ToString());
            int id_caso = int.Parse(TempData.Peek("id_caso").ToString());
            cSapModel subestacion = subes.Modelo;//INICIALIZAR EL SAP            
            Repositori.Guardar(subestacion, id_caso);
            subes.estadocorrerproyecto = true;
            return RedirectToAction("Index", "CargaSap");
        }
        public ActionResult correrproyecto()
        {
            int id = int.Parse(TempData.Peek("id").ToString());
            int id_caso = int.Parse(TempData.Peek("id_caso").ToString());
            cSapModel subestacion = subes.Modelo;//INICIALIZAR EL SAP            
            int ret = 0;
            Repositori.Guardar(subestacion, id_caso);
            ret = subestacion.Analyze.SetRunCaseFlag("MODAL", false,false);
            ret = subestacion.Analyze.RunAnalysis();
            ret = subestacion.File.Save();
            subes.estadoreaccionesservicio = true;
            subes.estadoreaccionesultimas = true;
            subes.estadoreaccionespernos = true;
            subes.estadocorrerdiseño = true;
            
            return RedirectToAction("Index", "CargaSap");
        }
        public ActionResult correrdiseño()
        {
            int id = int.Parse(TempData.Peek("id").ToString());
            int id_caso = int.Parse(TempData.Peek("id_caso").ToString());
            cSapModel subestacion = subes.Modelo;//INICIALIZAR EL SAP            
            Repositori.diseño(subestacion);
            subes.estadoresultadosdiseño = true;
            int ret = 0;
            ret = subestacion.File.Save();
            return RedirectToAction("Index", "CargaSap");
        }

        [HttpPost]
        public ActionResult reaccionesservicio()
        {
            int id = int.Parse(TempData.Peek("id").ToString());
            int id_caso = int.Parse(TempData.Peek("id_caso").ToString());
            #region REACCIONES DE SERVICIO
            List<Combinacionesservicio> combi = entidadesBD.Combinacionesservicios.ToList().FindAll(x => x.Caso == id_caso);
            //borrar combinaciones anteriores 
            foreach (Combinacionesservicio item in combi)
            {
                entidadesBD.Combinacionesservicios.Remove(item);
                entidadesBD.SaveChanges();
            }


            cSapModel subestacion = subes.Modelo;//INICIALIZAR EL SAP            
            int ret = 0;
            ret = subestacion.Results.Setup.DeselectAllCasesAndCombosForOutput();

            for(int i = 0; i < 14; i++)
            {
                string combinacion = "COMBS" + (i+1);
                ret = subestacion.Results.Setup.SetComboSelectedForOutput(combinacion);
            }
           


            int NumberResults=0;
            string[] Obj=null;            
            string[] Elm= null;
            string[] LoadCases = null;
            string[] StepType = null;
            double[] StepNum = null;
            double[] F1 = null;
            double[] F2 = null;
            double[] F3 = null;
            double[] M1 = null;
            double[] M2 = null;
            double[] M3 = null;

            ret = subestacion.Results.JointReact("Apoyos", eItemTypeElm.GroupElm, ref NumberResults, ref Obj, ref Elm, ref LoadCases, ref StepType, ref StepNum, ref F1, ref F2, ref F3, ref M1, ref M2, ref M3);
                        
            int nelem = F1.Count();
            for(int i=0;i<nelem;i++)
            {
                Combinacionesservicio com = new Combinacionesservicio();
                com.Joint = Obj[i];
                com.Combinacion = LoadCases[i];
                com.F1 = F1[i];
                com.F2 = F2[i];
                com.F3 = F3[i];
                com.M1 = M1[i];
                com.M2 = M2[i];
                com.M3 = M3[i];
                com.Caso = id_caso;
                if (ModelState.IsValid)
                {
                    entidadesBD.Combinacionesservicios.Add(com);
                    entidadesBD.SaveChanges();                    
                }

            }
            #endregion
            reaccionesultimas();
            reaccionespernos();            
            return RedirectToAction("Index", "CargaSap");
        }
        public void reaccionesultimas()
        {
            int id = int.Parse(TempData.Peek("id").ToString());
            int id_caso = int.Parse(TempData.Peek("id_caso").ToString());
            List<Combinacionesultima> combi = entidadesBD.Combinacionesultimas.ToList().FindAll(x => x.Caso == id_caso);
            //borrar combinaciones anteriores 
            foreach (Combinacionesultima item in combi)
            {
                entidadesBD.Combinacionesultimas.Remove(item);
                entidadesBD.SaveChanges();
            }


            cSapModel subestacion = subes.Modelo;//INICIALIZAR EL SAP            
            int ret = 0;
            ret = subestacion.Results.Setup.DeselectAllCasesAndCombosForOutput();

            for (int i = 0; i < 14; i++)
            {
                string combinacion = "COMB" + (i + 1);
                ret = subestacion.Results.Setup.SetComboSelectedForOutput(combinacion);
            }



            int NumberResults = 0;
            string[] Obj = null;
            string[] Elm = null;
            string[] LoadCases = null;
            string[] StepType = null;
            double[] StepNum = null;
            double[] F1 = null;
            double[] F2 = null;
            double[] F3 = null;
            double[] M1 = null;
            double[] M2 = null;
            double[] M3 = null;

            ret = subestacion.Results.JointReact("Apoyos", eItemTypeElm.GroupElm, ref NumberResults, ref Obj, ref Elm, ref LoadCases, ref StepType, ref StepNum, ref F1, ref F2, ref F3, ref M1, ref M2, ref M3);

            int nelem = F1.Count();
            for (int i = 0; i < nelem; i++)
            {
                Combinacionesultima com = new Combinacionesultima();
                com.Joint = Obj[i];
                com.Combinacion = LoadCases[i];
                com.F1 = F1[i];
                com.F2 = F2[i];
                com.F3 = F3[i];
                com.M1 = M1[i];
                com.M2 = M2[i];
                com.M3 = M3[i];
                com.Caso = id_caso;
                if (ModelState.IsValid)
                {
                    entidadesBD.Combinacionesultimas.Add(com);
                    entidadesBD.SaveChanges();
                }

            }            
        }
        public void reaccionespernos()
        {
            int id = int.Parse(TempData.Peek("id").ToString());
            int id_caso = int.Parse(TempData.Peek("id_caso").ToString());
            List<Combinacionespernosoequipos> combi = entidadesBD.Combinacionespernosoequipos.ToList().FindAll(x => x.Caso == id_caso);
            //borrar combinaciones anteriores 
            foreach (Combinacionespernosoequipos item in combi)
            {
                entidadesBD.Combinacionespernosoequipos.Remove(item);
                entidadesBD.SaveChanges();
            }


            cSapModel subestacion = subes.Modelo;//INICIALIZAR EL SAP            
            int ret = 0;
            ret = subestacion.Results.Setup.DeselectAllCasesAndCombosForOutput();
            string combinacion;
            for (int i = 0; i < 5; i++)
            {
                combinacion = "COMB" + (i + 1);
                ret = subestacion.Results.Setup.SetComboSelectedForOutput(combinacion);
            }
            for (int i = 5; i < 13; i++)
            {
                combinacion = "COMBE" + (i + 1);
                ret = subestacion.Results.Setup.SetComboSelectedForOutput(combinacion);
            }
            combinacion = "COMB" + 14;
            ret = subestacion.Results.Setup.SetComboSelectedForOutput(combinacion);


            int NumberResults = 0;
            string[] Obj = null;
            string[] Elm = null;
            string[] LoadCases = null;
            string[] StepType = null;
            double[] StepNum = null;
            double[] F1 = null;
            double[] F2 = null;
            double[] F3 = null;
            double[] M1 = null;
            double[] M2 = null;
            double[] M3 = null;

            ret = subestacion.Results.JointReact("Apoyos", eItemTypeElm.GroupElm, ref NumberResults, ref Obj, ref Elm, ref LoadCases, ref StepType, ref StepNum, ref F1, ref F2, ref F3, ref M1, ref M2, ref M3);

            int nelem = F1.Count();
            for (int i = 0; i < nelem; i++)
            {
                Combinacionespernosoequipos com = new Combinacionespernosoequipos();
                com.Joint = Obj[i];
                com.Combinacion = LoadCases[i];
                com.F1 = F1[i];
                com.F2 = F2[i];
                com.F3 = F3[i];
                com.M1 = M1[i];
                com.M2 = M2[i];
                com.M3 = M3[i];
                com.Caso = id_caso;
                if (ModelState.IsValid)
                {
                    entidadesBD.Combinacionespernosoequipos.Add(com);
                    entidadesBD.SaveChanges();
                }

            }
            
        }



        public ActionResult resultadosdiseño()
        {

            return RedirectToAction("Index", "CargaSap");
        }
        public ActionResult salirsap()
        {
            int ret = 0;
            cSapModel subestacion = subes.Modelo;
            ret = subestacion.File.Save();
            Repositori.salir(subes);
            subes.estadocorrerdiseño = false;
            subes.estadocorrerproyecto = false;
            subes.estadoguardar= false;
            subes.estadosalir = false;
            subes.estadoreaccionesservicio = false;
            subes.estadoreaccionesultimas = false;
            subes.estadoreaccionespernos = false;
            subes.estadoresultadosdiseño = false;
            return RedirectToAction("Index", "CargaSap");
        }


    }
}