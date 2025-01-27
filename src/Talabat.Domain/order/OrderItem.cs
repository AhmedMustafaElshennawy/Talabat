﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.baseEntity;
using Talabat.Domain.product;

namespace Talabat.Domain.order
{
    public class OrderItem:BaseEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } 
        public string PictureUrl { get; set; } 
        public decimal Price { get; set; } 
        public int Quantity { get; set; }
        public Product Product { get; set; }

    }
}
