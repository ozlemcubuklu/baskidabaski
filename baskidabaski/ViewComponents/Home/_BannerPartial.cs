using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.ViewComponents.Home
{
	public class _BannerPartial: ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
