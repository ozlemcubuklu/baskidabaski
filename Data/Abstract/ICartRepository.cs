using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface ICartRepository:IGenericRepository<Cart>
    {
        void ClearCart(int cartId);
        void DeleteFromCart(int CartId, int productId);
        Cart GetCartByUserId(string userId);
    }
}
