using Microsoft.AspNetCore.Mvc;
using Ecommence.Application.Common.Interfaces;
using Ecommence.Application.OrderServices;
using Ecommence.Infrastructure.Http;
using System.Net;
using System.Threading.Tasks;

namespace TicketTypePromotion.UI.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateOrder")]
        [ProducesResponseType(typeof(HttpResponseObjectSuccess<OrderDto>), (int)HttpStatusCode.OK),
         ProducesResponseType(typeof(HttpResponseObjectError<OrderDto>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<HttpResponseObject<OrderDto>>> CreateOrder(OrderDto order)
        {
            var reservationDto = await _orderService.CreateOrder(order);

            return Ok(reservationDto);
        }


    }
}
