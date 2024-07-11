using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.ViewComponents.AdminDashboard
{
    public class _AdminFooter:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
