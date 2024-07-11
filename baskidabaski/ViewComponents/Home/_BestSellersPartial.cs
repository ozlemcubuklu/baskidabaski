using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.ViewComponents.Home
{
	public class _BestSellersPartial:ViewComponent
	{
		public IViewComponentResult Invoke() { return View(); }
	}
}
