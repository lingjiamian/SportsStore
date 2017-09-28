using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace SportsStore.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

          
        }
    }
}
