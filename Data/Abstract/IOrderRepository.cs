﻿using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        List<Order> GetOrders(string userId);
    }
}
