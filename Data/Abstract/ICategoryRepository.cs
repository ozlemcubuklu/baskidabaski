using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        List<Category> GetPopularCategories();
        Category GetByIdWithProducts(int id);
        void DeletefromCategory(int ProductId, int Id);
    }
}
