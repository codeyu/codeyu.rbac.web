using System.Web.Mvc;

namespace Rbac.Web.Areas.Generator
{
    public class GeneratorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Generator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Generator_default",
                "Generator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
