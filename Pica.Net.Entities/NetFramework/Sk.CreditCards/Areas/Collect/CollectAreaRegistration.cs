using System.Web.Mvc;

namespace Ks.CreditCards.Areas.Collect
{
    public class CollectAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Collect";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Collect_default",
                "Collect/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}