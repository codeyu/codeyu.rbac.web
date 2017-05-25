using System.Web.Mvc;

namespace Murphy.Web.Areas.ReportCenter
{
    public class ReportCenterAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ReportCenter";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ReportCenter_default",
                "ReportCenter/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
