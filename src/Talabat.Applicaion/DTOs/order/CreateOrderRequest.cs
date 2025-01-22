using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.order;

namespace Talabat.Applicaion.DTOs.order
{
    public class CreateOrderRequest
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public OrderAddress ShippingAddress { get; set; }  // Make sure this is here
    }
}
