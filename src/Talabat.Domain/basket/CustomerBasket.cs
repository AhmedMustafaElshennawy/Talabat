using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Domain.basket
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItem>? BasketItems { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; } 
        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
