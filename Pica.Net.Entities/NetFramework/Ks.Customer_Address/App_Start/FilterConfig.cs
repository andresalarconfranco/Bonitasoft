using System.Web;
using System.Web.Mvc;

namespace Ks.Customer_Address
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
