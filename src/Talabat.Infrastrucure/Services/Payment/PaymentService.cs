using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Applicaion.Common.Interfaces.UnitOfWork;
using Talabat.Applicaion.Services.Payment;
using Talabat.Domain.basket;
using Talabat.Domain.order;

namespace Talabat.Infrastructure.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly StripeSettings _stripeSettings;
        private readonly IBasketRepository _basketRepository;
        private readonly IBaseRepository<DeliveryMethod> _DeliveryMethodRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(IOptions<StripeSettings> stripeSettings,
                    IBasketRepository basketRepository,
                    IBaseRepository<DeliveryMethod> DeliveryMethodRepository,
                    IProductRepository productRepository,
                    IUnitOfWork unitOfWork) 
        { 
            _stripeSettings = stripeSettings.Value; 
            _basketRepository = basketRepository;
            _DeliveryMethodRepository = DeliveryMethodRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = _stripeSettings.Secretkey;
            var basket = await _basketRepository.GetBasketAsync(BasketId);
            if (basket is null) return null!;

            //Amount = SubTotal + DeliveryMethod Coast
            var shippingCost = 0M;
            if (basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await _DeliveryMethodRepository.GetByIdAsync(basket.DeliveryMethodId.Value);
                shippingCost = DeliveryMethod.Cost;
            }
            if (basket.BasketItems!.Count > 0)
            {
                foreach (var item in basket.BasketItems)
                {
                    var product = await _productRepository.GetByIdAsync(item.Id);
                    if (product.Price != item.Price)
                        item.Price = product.Price;
                }
            }
            var SubTotal = basket.BasketItems.Sum(item => item.Price * item.Quantity);
            var Amount = SubTotal + shippingCost;

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)Amount * 100,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "Card" }
                };
                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)Amount * 100,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "Card" }
                };
                paymentIntent = await service.UpdateAsync(BasketId, options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            await _basketRepository.UpdateBasketAsync(basket);
            await _unitOfWork.CompleteAsync();
            return basket;
        }
    }
}
