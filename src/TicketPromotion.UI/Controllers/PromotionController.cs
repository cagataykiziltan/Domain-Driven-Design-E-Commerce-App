using Microsoft.AspNetCore.Mvc;
using TicketTypePromotion.Application.Common.Interfaces;
using TicketTypePromotion.Application.PromotionServices;


namespace TicketTypePromotion.UI.Controllers
{
    [Route("api/[controller]")]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }
     

        [HttpPost("CreatePromotion")]
        public ActionResult CreatePromotion(PromotionDto promotion)
        {
            var promotionDto = _promotionService.CreatePromotion(promotion);
          
            return Ok(promotionDto);
        }

        [HttpGet("GetPromotionInfo/{promotionName}")]
        public ActionResult GetPromotionInfo(string promotionName)
        {
            var promotionDto = _promotionService.GetPromotionByName(promotionName);

            return Ok(promotionDto);
        }
           
    }
}
