using MediatR;
using Microsoft.AspNetCore.Mvc;
using Talabat.Applicaion.Features.basket.Commands.CreateCustomerBaskets;
using Talabat.Applicaion.Features.basket.Commands.DeleteCustomerBaskets;
using Talabat.Applicaion.Features.basket.Queries.GetCustomerBasket;
using Talabat.Domain.basket;

namespace Talabat.API.Controllers
{
    [ApiController]
    public class BasketsController : ApiController
    {
        private readonly ISender _sender;
        public BasketsController(ISender sender) => _sender = sender;
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomerBaskets(string Id)
        {
            var query = new GetCustomerBasketQuery(Id);
            var result = await _sender.Send(query);
            var response = result.Match(
                Success => Ok(result.Value),
                error => Problem(error)
            );

            return response;
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCustomerBaskets(string Id)
        {
            var query = new DeleteCustomerBasketsCommand(Id);
            var result = await _sender.Send(query);
            var response = result.Match(
                Success => Ok(result.Value),
                error => Problem(error)
            );

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomerBaskets(CustomerBasket customerBasket)
        {
            var query = new UpdateCustomerBasketsCommand(customerBasket);
            var result = await _sender.Send(query);
            var response = result.Match(
                Success => Ok(result.Value),
                error => Problem(error)
            );

            return response;
        }
    }
}