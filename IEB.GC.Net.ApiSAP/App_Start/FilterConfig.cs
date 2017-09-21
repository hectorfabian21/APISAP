using System.Web;
using System.Web.Mvc;

namespace IEB.GC.Net.ApiSAP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
