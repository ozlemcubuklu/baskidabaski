using baskidabaski.Extensitons;
using baskidabaski.Models;
using Business.Abstract;
using Business.Concrete;
using Data.Concrete.EfCore;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace baskidabaski.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ProductManager productManager = new ProductManager(new EfCoreProductRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCoreCategoryRepository());
        public PartialViewResult PartialHeader()
        {
            return PartialView();
        }

        public PartialViewResult PartialSolSideBar()
        {
            return PartialView();
        }

        public PartialViewResult PartialSagSideBar()
        {
            return PartialView();
        }

        public PartialViewResult PartialScript()
        {
            return PartialView();
        }
        public PartialViewResult PartialFooter()
        {
            return PartialView();
        }
        public IActionResult Index()
        {
            return View();

        }
        public IActionResult ProductList()
        {
            var values = productManager.GetAll();
            ProductListViewModel urunler = new ProductListViewModel() { Products = values };

            return View(urunler);
        }
        public IActionResult productdelete(int id)
        {
            var entity = productManager.GetById(id);
            if (entity != null)
            {
                productManager.Delete(entity);
            }

            return RedirectToAction("ProductList");
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
                    productManager.Create(p);

                    return RedirectToAction("ProductList");

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
            var entity = productManager.GetByIdWithCategories((int)id);
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
            ViewBag.categories = categoryManager.GetAll();
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel model, int[] categoryIds)
        {

           
            //if (ModelState.IsValid)
            //{
            //    if (model == null)
            //    {
            //        return NotFound(ModelState);

            //    }
            var entity = productManager.GetById(model.ProductId);

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
            productManager.Update(entity,categoryIds);
        

            return RedirectToAction("ProductList");
           
        }


        public IActionResult CategoryList()
        {
            return View(new CategoryListViewModel()
            {
                Categories = categoryManager.GetAll()
            });
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
                categoryManager.Create(c);
				TempData.Put("message", new AlertMessage() { AlertType = "success", Title = "Kategori Ekleme", Message = $"{model.Name} adlı kategori eklendi." });


				return RedirectToAction("CategoryList");
            }
            return View(model);

        }


        
        public IActionResult DeleteCategory(int id)
        {
            var entity = categoryManager.GetById(id);
            if (entity != null)
            {
                categoryManager.Delete(entity);
            }
			TempData.Put("message", new AlertMessage() { AlertType = "danger", Title = "Kategori Silme", Message = $"{entity.Name} adlı kategori silindi." });

			return RedirectToAction("CategoryList");
        }


        [HttpGet]
        public IActionResult categoryupdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = categoryManager.GetByIdWithProducts((int)id);
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
                var entity = categoryManager.GetById(model.Id);

                if (entity == null)
                {
                    return BadRequest(ModelState);
                }

                entity.Name = model.Name;

                entity.Id = model.Id;
                categoryManager.Update(entity);
				TempData.Put("message", new AlertMessage() { AlertType = "success", Title = "Kategori Güncelleme", Message = $"{model.Name} adlı kategori gğncellendi." });


				return RedirectToAction("CategoryList");
            }
            return View(model);

        }



        [HttpPost]
        public IActionResult DeletefromCategory(int ProductId, int CategoryId)
        {

            categoryManager.DeletefromCategory(ProductId, CategoryId);
            return Redirect("/admin/categoryupdate/" + CategoryId);
        }


    
      

    } 
}
