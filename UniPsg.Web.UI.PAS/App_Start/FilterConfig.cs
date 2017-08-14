using System.Web;
using System.Web.Mvc;

namespace UniPsg.Web.UI.PAS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
