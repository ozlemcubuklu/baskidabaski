using Data.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category>, ICategoryRepository
    {
        public void DeletefromCategory(int ProductId, int Id)
        {
            Context context = new Context();
            var cmd = "Delete from productcategory where ProductId=@p0 and CategoryId=@p1";
            context.Database.ExecuteSqlRaw(cmd, ProductId, Id);
        }

        public Category GetByIdWithProducts(int id)
        {

            Context context = new Context();
            return context.Categories.Where(i => i.Id == id)
                .Include(i => i.ProductCategories)
                .ThenInclude(i => i.Product).FirstOrDefault();
        }

        public List<Category> GetPopularCategories()
        {
            throw new NotImplementedException();
        }
    }
}
