using System.Web.Mvc;

namespace Murphy.Web.Areas.InfoCenter
{
    public class InfoCenterAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "InfoCenter";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "InfoCenter_default",
                "InfoCenter/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
