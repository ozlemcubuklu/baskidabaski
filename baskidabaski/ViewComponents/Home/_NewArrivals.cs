using baskidabaski.Models;
using Business.Concrete;
using Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.ViewComponents.Home
{
	public class _NewArrivals: ViewComponent
    {
        ProductManager productManager = new ProductManager(new EfCoreProductRepository());

        public IViewComponentResult Invoke()
        {
            var products = productManager.GethomePageProducts();
            return View(products);
                }


    }
}
