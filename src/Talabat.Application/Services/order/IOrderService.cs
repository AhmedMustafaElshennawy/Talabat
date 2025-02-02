using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.order;

namespace Talabat.Applicaion.Services.order
{
    public interface IOrderService
    {
        public Task<Order?> CreateOrderAsync(string BayerEmail, string BasketId, int DeliveryMethodId, OrderAddress adress);
    }
}
