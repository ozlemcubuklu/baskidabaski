using baskidabaski.Extensitons;
using baskidabaski.Models;
using Business.Abstract;
using Business.Concrete;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.Areas.Admin.Controllers
{ [Area("Admin")]

    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var values=_categoryService.GetAll();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                Category c = new Category()
                {
                    Name = model.Name,

                };
                _categoryService.Create(c);
               
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View(model);
        }



        public IActionResult DeleteCategory(int id)
        {
            var entity = _categoryService.GetById(id);
            if (entity != null)
            {
                _categoryService.Delete(entity);
            }
             return RedirectToAction("Index", "Category", new { area = "Admin" });
        }


        [HttpGet]
        public IActionResult categoryupdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _categoryService.GetByIdWithProducts((int)id);
            if (entity == null)
            {
                return NotFound();
            }

            var model = new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,

                Products = entity.ProductCategories.Select(i => i.Product).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult categoryupdate(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                if (model == null)
                {
                    return NotFound(ModelState);

                }
                var entity = _categoryService.GetById(model.Id);

                if (entity == null)
                {
                    return BadRequest(ModelState);
                }

                entity.Name = model.Name;

                entity.Id = model.Id;
                _categoryService.Update(entity);

                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View(model);

        }



        [HttpPost]
        public IActionResult DeletefromCategory(int ProductId, int CategoryId)
        {

            _categoryService.DeletefromCategory(ProductId, CategoryId);
            return Redirect("/admin/category/categoryupdate/" + CategoryId);
        }
    }
}
