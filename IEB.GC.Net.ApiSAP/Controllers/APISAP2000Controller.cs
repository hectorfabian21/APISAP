using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAP2000v19;
using IEB.GC.Net.ApiSAP.Models;

namespace IEB.GC.Net.ApiSAP.Controllers
{
    public class APISAP2000Controller : Controller
    {
        public static Repositorio Repositori = new Repositorio();
        APISAPEntities1 entidadesBD = new APISAPEntities1();
        // GET: APISAP2000
        public ActionResult Index()
        {

            ElemtosSAP subes = Repositori.abrir_sap2000();
            cSapModel subestacionmodelo =subes.Modelo;//INICIALIZAR EL SAP
            Repositori.Material_default(subestacionmodelo);//crea el material default                                  
            string Nivel_tension; 
            
            List<Elemento> columnas = entidadesBD.Elementos.ToList();
            Elemento col = columnas.Find(x => x.Id==4); // Columna > Id == 2 (fijo)
            Nivel_tension = col.NivelTension;
            Repositori.Perfiles_default(Nivel_tension, subestacionmodelo);//crea perfileria por default

            Default_T_Carga sismo_viento = new Default_T_Carga();

            sismo_viento.Cortante_basal = 0.2;
            sismo_viento.K_exponente = 1;
            sismo_viento.Velocidad_viento = 103.13;
            sismo_viento.Tipo_exposicion = 2;//modificar dado que se clasifica B=1 C=2 y D=3
            sismo_viento.Factor_importancia = 1;
            sismo_viento.Factor_topografia = 1.06;
            sismo_viento.Factor_rafaga = 0.996;
            sismo_viento.Factor_direccion = 1;
                        
            Repositori.Default_T_Cargas(subestacionmodelo,sismo_viento);
            Repositori.Mass_Source(subestacionmodelo,-1);
            Repositori.Default_combinaciones(subestacionmodelo, sismo_viento);

            Ensamble_subestacion referencia = new Ensamble_subestacion();
            List<Ensamble_subestacion> ensamble1 = entidadesBD.Ensamble_subestacions.ToList<Ensamble_subestacion>();
            List<Ensamble_subestacion> ensamble = ensamble1.FindAll(x => x.id_Subestacion == 1);

            foreach (Ensamble_subestacion re in ensamble)
            {

                if (re.Elemento.ToUpper().StartsWith("C"))
                {
                    Repositori.crear_columna(re, subestacionmodelo);//Crea columnas
                }
                else if(re.Elemento.ToUpper().StartsWith("V"))
                {
                    Repositori.crear_viga(re, subestacionmodelo);//Crea columnas
                }
                else if(re.Elemento.ToUpper().StartsWith("E"))
                {

                }
                else
                {
                    //TODO nada
                }

            }
                         
            
            return RedirectToAction("Index", "Elemento");

        }

    }
}