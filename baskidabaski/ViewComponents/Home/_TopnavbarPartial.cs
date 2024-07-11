using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.ViewComponents.Home
{
    public class _TopnavbarPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
