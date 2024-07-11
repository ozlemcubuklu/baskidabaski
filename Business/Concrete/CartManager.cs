using Business.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CartManager : ICartService
    {
        public void AddToCart(string userId, int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public void ClearCart(int CartId)
        {
            throw new NotImplementedException();
        }

        public void DeleteFromCart(string userId, int productId)
        {
            throw new NotImplementedException();
        }

        public Cart GetCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public void InitializeCart(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
