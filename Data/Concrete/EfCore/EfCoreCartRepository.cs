using Data.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete.EfCore
{
    public class EfCoreCartRepository : EfCoreGenericRepository<Cart>, ICartRepository
    {
        public void ClearCart(int cartId)
        {
            throw new NotImplementedException();
        }

        public void DeleteFromCart(int CartId, int productId)
        {
            throw new NotImplementedException();
        }

        public Cart GetCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
