using Business.Abstract;
using Data.Abstract;
using Data.Concrete;
using Entity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductRepository _productrepository;

        public ProductManager(IProductRepository repository)
        {
            _productrepository = repository;
        }

        public void Create(Product entity)
        {
            _productrepository.Create(entity);
        }

        public void Delete(Product entity)
        {
           _productrepository.Delete(entity);
        }

        public List<Product> GetAll()
        {
           return _productrepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productrepository.GetById(id);
        }

        public Product GetByIdWithCategories(int id)
        {
         return _productrepository.GetByIdWithCategories(id);
        }

        public int getCountByCategory(string category)
        {
           return _productrepository.getCountByCategory(category);
        }

        public List<Product> GethomePageProducts()
        {
           return _productrepository.GethomePageProducts();
        }

        public Product GetProductDetails(int id)
        {
            return _productrepository.GetProductDetails(id);
        }

        public List<Product> GetProductsByCategory(string category, int page, int pagesize)
        {
            return _productrepository.GetProductsByCategory(category, page, pagesize);
        }

        public List<Product> SearchResult(string q)
        {
            return _productrepository.SearchResult(q);
        }

        public void Update(Product entity, int[] categoryIds)
        {
        
                _productrepository.Update(entity, categoryIds);
               
               
        }
        public string ErrorMessage { get; set; }

        public bool Validation(Product entity)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(entity.Name))
            {

                isValid = false;
                ErrorMessage += "ürün ismi girmelisiniz.\n";
            }

            if (entity.Price < 0)
            {
                isValid = false;
                ErrorMessage += "ürün fiyatı sıfırdan küçük olamaz.\n";
            }
            return isValid;
        }


        public void Update(Product entity)
        {
            _productrepository.Update(entity);  
        }
    }
}
