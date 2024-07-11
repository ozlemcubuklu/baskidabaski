using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService:IGenericService<Product>
    {
        List<Product> GetProductsByCategory(string category, int page, int pagesize);
 
        Product GetProductDetails(int id);
        int getCountByCategory(string category);
        List<Product> GethomePageProducts();
        List<Product> SearchResult(string q);
        Product GetByIdWithCategories(int id);
        void Update(Product entity, int[] categoryIds);
    }
}
