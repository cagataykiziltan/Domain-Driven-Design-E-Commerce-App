using Microsoft.AspNetCore.Mvc;
using TicketTypePromotion.Application.Common.Interfaces;
using TicketTypePromotion.Application.ReservationServices;

namespace TicketTypePromotion.UI.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("CreateReservation")]
        public ActionResult CreateReservation(ReservationDto reservation)
        {
            var reservationDto = _reservationService.CreateReservation(reservation);

            return Ok(reservationDto);
        }


    }
}
