using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAP2000v19;

namespace IEB.GC.Net.ApiSAP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ActionResult result = null;
            #region Acceso SAP
           


            #endregion

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

}