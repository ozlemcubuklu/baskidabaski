using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.ViewComponents.AdminDashboard
{
	public class _AdminHead:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
