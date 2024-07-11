using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.ViewComponents.AdminDashboard
{
	public class _AdminSideBar:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
