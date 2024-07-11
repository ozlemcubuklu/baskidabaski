using Business.Abstract;
using Data.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Create(Category entity)
        {
          _categoryRepository.Create(entity);
        }

        public void Delete(Category entity)
        {
           _categoryRepository.Delete(entity);
        }

        public void DeletefromCategory(int ProductId, int Id)
        {
            _categoryRepository.DeletefromCategory(ProductId, Id);
        }

        public List<Category> GetAll()
        {
         return _categoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public Category GetByIdWithProducts(int id)
        {
           return _categoryRepository.GetByIdWithProducts(id);
        }

        public void Update(Category entity)
        {
            _categoryRepository.Update(entity); 
        }
    }
}
