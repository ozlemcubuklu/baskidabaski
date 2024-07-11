using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.ViewComponents.AdminDashboard
{
	public class _AdminNavbarUser:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
