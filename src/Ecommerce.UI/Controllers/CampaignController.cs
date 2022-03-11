using Microsoft.AspNetCore.Mvc;
using Ecommence.Application.Common.Interfaces;
using Ecommence.Application.CampaignServices;
using Ecommence.Infrastructure.Http;
using System.Threading.Tasks;
using System.Net;

namespace TicketTypePromotion.UI.Controllers
{
    [Route("api/[controller]")]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;
        
        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }
     

        [HttpPost("CreateCampaign")]
        [ProducesResponseType(typeof(HttpResponseObjectSuccess<CampaignDto>), (int)HttpStatusCode.OK),
         ProducesResponseType(typeof(HttpResponseObjectError<CampaignDto>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<HttpResponseObject<CampaignDto>>> CreateCampaign(HttpRequestObject<CampaignDto> campaign)
        {
            var promotionDto = await _campaignService.CreateCampaign(campaign.Items.First());
          
            return Ok(promotionDto);
        }

        [HttpGet("GetPromotionInfo/{campaignnName}")]
        [ProducesResponseType(typeof(HttpResponseObjectSuccess<CampaignDto>), (int)HttpStatusCode.OK),
         ProducesResponseType(typeof(HttpResponseObjectError<CampaignDto>), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<HttpResponseObject<CampaignDto>>> GetPromotionInfo(HttpRequestObject<string> campaignName)
        {
            var promotionDto = await _campaignService.GetCampaignByName(campaignName.Items.First());

            return Ok(promotionDto);
        }
           
    }
}
