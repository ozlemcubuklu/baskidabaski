using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.ViewComponents.Home
{
    public class _sliderPartial : ViewComponent
    {
        public IViewComponentResult Invoke() { return View(); }
    }
}
