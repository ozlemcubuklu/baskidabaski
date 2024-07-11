using baskidabaski.Models;
using Business.Abstract;
using Business.Concrete;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace baskidabaski.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var values = _productService.GetAll();
            return View(values);
        }
        public IActionResult productdelete(int id)
        {
            var entity = _productService.GetById(id);
            if (entity != null)
            {
                _productService.Delete(entity);
            }
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
        [HttpGet]
        public IActionResult ProductCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {

            if (model.Image1 != null || model.Image2 != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension1 = Path.GetExtension(model.Image1.FileName);
                var extension2 = Path.GetExtension(model.Image2.FileName);

                var imagename1 = Guid.NewGuid().ToString() + extension1;
                var imagename2 = Guid.NewGuid().ToString() + extension2;

                var savelocation1 = resource + "/wwwroot/image/productimg/" + imagename1;
                var savelocation2 = resource + "/wwwroot/image/productimg/" + imagename2;

                if (ModelState.IsValid)
                {
                    var stream1 = new FileStream(savelocation1, FileMode.Create);
                    var stream2 = new FileStream(savelocation2, FileMode.Create);
                    await model.Image1.CopyToAsync(stream1);
                    await model.Image2.CopyToAsync(stream2);
                    Product p = new Product()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        image1 = imagename1,
                        image2 = imagename2,
                        Price = model.Price,
                        IsApproved = model.IsApproved,
                        IsHome = model.IsHome
                    };
                    _productService.Create(p);
                    return RedirectToAction("Index", "Product", new { area = "Admin" });

                }
            }

            return View(model);

        }

        [HttpGet]
        public IActionResult ProductUpdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _productService.GetByIdWithCategories((int)id);
            if (entity == null)
            {
                return NotFound();
            }

            var model = new ProductModel()
            {
                ProductId = entity.Id,
                Name = entity.Name,
                Description = entity.Description,

                Image1String = entity.image1,
                Image2String = entity.image2,
                Price = entity.Price,
                IsApproved = entity.IsApproved,
                IsHome = entity.IsHome,
                SelectedCategories = entity.ProductCategories.Select(i => i.Category).ToList()

            };
            ViewBag.categories = _categoryService.GetAll();
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel model, int[] categoryIds)
        {


            if (!ModelState.IsValid)
            {
                ViewBag.categories = _categoryService.GetAll();
                return View(model);
            }
            var entity = _productService.GetById(model.ProductId);

            if (entity == null)
            {
                return BadRequest(ModelState);
            }

            entity.Id = model.ProductId;
            entity.Name = model.Name;
            entity.Description = model.Description;

            entity.Price = model.Price;
            entity.IsApproved = model.IsApproved;
            entity.IsHome = model.IsHome;

            if (model.Image1 != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extention = Path.GetExtension(model.Image1.FileName);
                var randomname = string.Format($"{Guid.NewGuid()}{extention}");
                var savelocation1 = resource + "/wwwroot/image/productimg/" + randomname;

                entity.image1 = randomname;

                using (var stream = new FileStream(savelocation1, FileMode.Create))
                {
                    await model.Image1.CopyToAsync(stream);
                }
            }

            if (model.Image2 != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extention = Path.GetExtension(model.Image2.FileName);
                var randomname = string.Format($"{Guid.NewGuid()}{extention}");
                var savelocation2 = resource + "/wwwroot/image/productimg/" + randomname;
                entity.image2 = randomname;

                using (var stream = new FileStream(savelocation2, FileMode.Create))
                {
                    await model.Image2.CopyToAsync(stream);
                }
            }
            _productService.Update(entity, categoryIds);


            return RedirectToAction("Index", "Product", new { area = "Admin" });

        }
       

    }
}
