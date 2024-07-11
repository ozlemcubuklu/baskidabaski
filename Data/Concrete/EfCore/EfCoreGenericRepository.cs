using Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete.EfCore
{
    public class EfCoreGenericRepository<T> : IGenericRepository<T> where T : class
    {


        public void Create(T entity)
        {
            using var context = new Context();
           
                context.Set<T>().Add(entity);
                context.SaveChanges();

            

        }

        public void Delete(T entity)
        {
            using var context = new Context();
            
                context.Set<T>().Remove(entity);
                context.SaveChanges();

            

        }

        public List<T> GetAll()
        {
            using var context = new Context();
            
                return context.Set<T>().ToList();
            



        }

        public virtual T GetById(int id)
        {
            using var context = new Context();
            
                return context.Set<T>().Find(id);
            

        }

        public virtual void Update(T entity)
        {
            using var context = new Context();
            
                context.Set<T>().Update(entity);
                context.SaveChanges();

            

        }
    }
}
