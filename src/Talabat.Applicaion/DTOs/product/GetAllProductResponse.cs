using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.productBrand;
using Talabat.Domain.productType;

namespace Talabat.Applicaion.DTOs.product
{
    public record GetAllProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public GetProductBrandResponse ProductBrand { get; set; }
        public GetProductTypeResponse ProductType { get; set; }
    }
    public record GetProductBrandResponse
    {
        public int Id { get; set; }
        public string ProductBrand { get; set; }

    } 
    public record GetProductTypeResponse
    {
        public int Id { get; set; }
        public string ProducType { get; set; }
    }
}
