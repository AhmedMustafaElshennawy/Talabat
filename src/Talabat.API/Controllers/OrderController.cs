using MediatR;
using Microsoft.AspNetCore.Mvc;
using Talabat.Application.DTOs.order;
using Talabat.Application.Features.order.Commands.CreateOrder;
using Talabat.Application.Features.order.Queries.GetDeliveryMethods;
using Talabat.Application.Features.order.Queries.GetSpecificOrderForSpecificUser;
using Talabat.Application.Features.order.Queries.GetUserOrders;
using Talabat.Domain.order;

namespace Talabat.API.Controllers
{
    public class OrderController : ApiController
    {
        private readonly ISender _sernder;
        public OrderController(ISender sernder) => _sernder = sernder;

        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            var command = new CreateOrderCommand(request);
            var result = await _sernder.Send(command);

            var response = result.Match(
                success => Ok(result.Value),
                error=>Problem(error)
                );

            return response;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserOrders(string userId)
        {
            var query = new GetUserOrdersQuery(userId);
            var result = await _sernder.Send(query);

            var response = result.Match(
                success => Ok(result.Value),
                error => Problem(error)
                );

            return response;
        }
        [HttpGet]
        public async Task<IActionResult> GetSpecificOrderForSpecificUser(GetSpecificOrderRequest request)
        {
            var query = new GetSpecificOrderForSpecificUserQuery(
                request.BayerEmail,
                request.OrderId);

            var result = await _sernder.Send(query);
            var response = result.Match(
                success => Ok(result.Value),
                error => Problem(error)
                );

            return response;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(DeliveryMethod), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var query = new GetDeliveryMethodsQuery();

            var result = await _sernder.Send(query);
            var response = result.Match(
                success => Ok(result.Value),
                error => Problem(error)
                );

            return response;
        }
    }
}