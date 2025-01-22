using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.baseEntity;
using Talabat.Domain.identity;

namespace Talabat.Domain.order
{
    public class Order:BaseEntity
    {
        public string BayerEmail { get; set; } 
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int DeliveryMethodId { get; set; }
        public ICollection<OrderItem> Items { get; set; }= new HashSet<OrderItem>(); 
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public string UserId { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
    }
}