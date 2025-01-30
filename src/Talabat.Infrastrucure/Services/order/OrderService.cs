using Talabat.Applicaion.Common.Interfaces.Authentication.Repositories;
using Talabat.Applicaion.Common.Interfaces.UnitOfWork;
using Talabat.Applicaion.Services.Payment;
using Talabat.Domain.order;

namespace Talabat.Applicaion.Services.order
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBaseRepository<DeliveryMethod> _DeliveryMethodRepository;
        private readonly IBaseRepository<Order> _OrderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        public OrderService(IBasketRepository basketRepository,
            IProductRepository productRepository,
            IBaseRepository<DeliveryMethod> deliveryMethodRepository,
            IBaseRepository<Order> orderRepository,
            IUnitOfWork unitOfWork,
            IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _DeliveryMethodRepository = deliveryMethodRepository;
            _OrderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }

        public async Task<Order?> CreateOrderAsync(string BayerEmail, string BasketId, int DeliveryMethodId, OrderAddress adress)
        {
            var basket = await _basketRepository.GetBasketAsync(BasketId);

            var items = new List<OrderItem>();
            if (basket?.BasketItems!.Count>0)
            {
                foreach (var item in basket.BasketItems!)
                {
                    var product = await _productRepository.GetByIdAsync(item.Id);
                    var orderItem = new OrderItem
                    {
                        Id = item.Id,
                        PictureUrl = product.PictureUrl,
                        Price = product.Price,
                        Product = product,
                        ProductId = item.Id,
                        ProductName = product.Name
                    };
                }
            }

            var subtotal = items.Sum(item => item.Price * item.Quantity);
            var deliveryMethod = await _DeliveryMethodRepository.GetByIdAsync(DeliveryMethodId);
            var existingOrder = _OrderRepository.Entites()
                .FirstOrDefault(x => x.PaymentIntentId == basket!.PaymentIntentId);

            if (existingOrder is not null)
            {
                await _unitOfWork.Orders.DeleteAsync(existingOrder.Id);
                await _paymentService.CreateOrUpdatePaymentIntent(BasketId);
            }

            if (basket?.PaymentIntentId == null)
                 throw new InvalidOperationException("Basket PaymentIntentId is missing.");
            
            var order = new Order
            {
                BayerEmail = BayerEmail,
                DeliveryMethodId = DeliveryMethodId,
                ShippingAddress = adress,
                SubTotal = subtotal,
                DeliveryMethod = deliveryMethod,
                Items = items,
                PaymentIntentId =basket.PaymentIntentId
            };

            var result = await _OrderRepository.AddEntityAsync(order);
            await _unitOfWork.CompleteAsync();
            return result;
        }
    }
}