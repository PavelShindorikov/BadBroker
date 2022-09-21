using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Contracts;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]/[action]")]
    public class RatesController : ControllerBase
    {
        private readonly ITradeService _rateService;
        
        public RatesController(ITradeService rateService) => _rateService = rateService;

        /// <summary>
        /// Get best revenue
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /best?startDate=2014-12-15&endDate=2014-12-23&money=100
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Returns best revenue or not found</returns>
        /// <response code="200">Success</response>
        /// <response code="404">If not have best revenue</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ActionName("best")]
        public async Task<IActionResult> GetBestRevenue([FromQuery] BestRevenueRequest request)
        {
            try
            {
                var response = await _rateService.GetBestRevenueAsync(request);
                return Ok(response);
            }
            catch (RateServiceException e)
            {
                return Problem(e.Message, statusCode:500);
            }
        }
    }
}