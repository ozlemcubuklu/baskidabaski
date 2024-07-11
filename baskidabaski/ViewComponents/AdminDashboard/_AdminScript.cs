using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.ViewComponents.AdminDashboard
{
    public class _AdminScript:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
