using Business.Concrete;
using Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.ViewComponents.Shop
{
    public class _categorypartial:ViewComponent
    {
        CategoryManager categorymanager = new CategoryManager(new EfCoreCategoryRepository());
        public IViewComponentResult Invoke()
		{
			if (RouteData.Values["category"] != null)
				ViewBag.SelectedCategory = RouteData?.Values["category"];
			//return View(CategoryRepository.Categories);
			return View(categorymanager.GetAll());
		}
    }
}
