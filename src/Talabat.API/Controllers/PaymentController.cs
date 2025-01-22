using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Applicaion.DTOs.customerBasket;
using Talabat.Applicaion.Features.order.Commands.CreateOrUpdatePayment;

namespace Talabat.API.Controllers
{
    public class PaymentController : ApiController
    {
        private readonly ISender _sender;
        public PaymentController(ISender sender) => _sender = sender;
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrUpdatePayment(string BasketId)
        {
            var command = new CreateOrUpdatePaymentCommand(BasketId);
            var result = await _sender.Send(command);

            var response = result.Match(
                success=>Ok(result.Value),
                error=>Problem(error)
                );
            return response;
        }
    }
}
