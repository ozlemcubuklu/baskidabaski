﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.Areas.Admin.Controllers
{
	[Area("Admin")]

    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
