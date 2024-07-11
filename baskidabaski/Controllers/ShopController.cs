using baskidabaski.Models;
using Business.Abstract;
using Business.Concrete;
using Data.Concrete.EfCore;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace baskidabaski.Controllers
{
    public class ShopController : Controller
    {

        private readonly IProductService _productService;

      
        public ShopController(IProductService productService)
        {
            _productService = productService;
         
        }

        public IActionResult List(string category, int page = 1)
        {
            const int pagesize = 6;
            var productViewModel = new ProductListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    ItemsPerPage = pagesize,
                    TotalItems = _productService.getCountByCategory(category),
                    CurrentPage = page,
                    CurrentCategory = category
                },
                Products = _productService.GetProductsByCategory(category, page, pagesize)
            };

            return View(productViewModel);
        }



		public IActionResult Search(string q)
		{
            const int pagesize = 6;
            var productViewModel = new ProductListViewModel()
			{
              
                Products = _productService.SearchResult(q)
			};

			return View(productViewModel);
		}


        public IActionResult Details(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Product p = _productService.GetProductDetails(Id);
            if (p == null)
            {
                return NotFound();
            }

            return View(new ProductDetailsModel { Product = p, Categories = p.ProductCategories.Select(i => i.Category).ToList() });
        }
    }
}
