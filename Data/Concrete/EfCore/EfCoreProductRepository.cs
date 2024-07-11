using Data.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete.EfCore
{
    public class EfCoreProductRepository : EfCoreGenericRepository<Product>, IProductRepository
    {
    

        public Product GetByIdWithCategories(int id)
        {
            var context = new Context();
            return context.Products.Where(i => i.Id == id) .Include(i => i.ProductCategories).ThenInclude(i => i.Category).FirstOrDefault();
        }

        public int getCountByCategory(string category)
        {
            var context=new Context();
            var product = context.Products.Where(i => i.IsApproved).AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                product = product.Include(i => i.ProductCategories).ThenInclude(i => i.Category)
                                        .Where(i => i.ProductCategories.Any(a => a.Category.Name == category));
            }
            return product.Count();
        }

        public List<Product> GethomePageProducts()
        {
            using Context context = new Context();
            var product = context.Products.Where(i => i.IsApproved && i.IsHome).ToList();


            return product;
        }

        public List<Product> GetPopularProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetProductDetails(int id)
        {
            var context = new Context();
            var product = context.Products.Where(i=>i.Id==id).Include(i => i.ProductCategories).ThenInclude(i => i.Category).FirstOrDefault(); 
            return product;
        }
        public List<Product> GetProductsByCategory(string category, int page, int pagesize)
        {
            var context = new Context();
            var product = context.Products.Where(i => i.IsApproved).AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                product = product.Include(i => i.ProductCategories).ThenInclude(i => i.Category)
                                        .Where(i => i.ProductCategories.Any(a => a.Category.Name == category));
            }
            return product.Skip((page - 1) * pagesize).Take(pagesize).ToList();
        }

        public List<Product> GetTop5Products()
        {
            throw new NotImplementedException();
        }

        public List<Product> SearchResult(string? search)
        {
            using Context context=new Context();
            var product= context.Products.Where(i => i.IsApproved).AsQueryable();;
            if (!string.IsNullOrEmpty(search)) { 
			product = context.Products.Where(i => i.IsApproved && (i.Name.ToLower().Contains(search.ToLower()) || i.Description.ToLower().Contains(search.ToLower()))).AsQueryable();
}
           
            return product.ToList();
		}

        public void Update(Product entity, int[] categoryIds)
        {
           using var context=new Context();
                var product = context.Products.Include(i => i.ProductCategories).FirstOrDefault(i=>i.Id==entity.Id);
                if (product != null)
                {
                    
                    product.Price=entity.Price;
                    product.Description=entity.Description;
                    product.Name= entity.Name;
                product.image1 = entity.image1;
                product.image2 = entity.image2;
                    product.IsApproved=entity.IsApproved;
                    product.IsHome=entity.IsHome;
                    product.ProductCategories= categoryIds
                        .Select(catId => new ProductCategory() { ProductId = entity.Id, CategoryId = catId }).ToList();

                context.SaveChanges();
        }
        }
    }
}
