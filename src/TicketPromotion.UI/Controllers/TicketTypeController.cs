using Microsoft.AspNetCore.Mvc;
using TicketTypePromotion.Application.Common.Interfaces;
using TicketTypePromotion.Application.TicketTypeServices;

namespace TicketTypePromotion.UI.Controllers
{
    [Route("api/[controller]")]
    public class TicketTypeController : Controller
    {
        private readonly ITicketTypeService _ticketTypeService;
        public TicketTypeController(ITicketTypeService ticketTypeService)
        {
            _ticketTypeService = ticketTypeService;
        }
        [HttpPost("CreateTicketType")]
        public ActionResult CreateTicketType(TicketTypeDto ticketType)
        {
           var ticketTypeDto = _ticketTypeService.CreateTicketType(ticketType);
           
            return Ok(ticketTypeDto);
        }

        [HttpGet("GetTicketTypeInfo/{ticketTypeName}")]
        public ActionResult GetTicketTypeInfo(string ticketTypeName)
        {
            var ticketDto = _ticketTypeService.GetTicketType(ticketTypeName);

            return Ok(ticketDto);
        }

    }
}
