using HackerNews.BusinessLogic.Contracts;
using HackerNews.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackerNews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestStoriesController : ControllerBase
    {
        private readonly IBestStoryService _bestStoryService;
        public BestStoriesController(IBestStoryService bestStoryService) 
        {
            _bestStoryService = bestStoryService;
            
        }
        // GET: api/<BestStoriesController>
        [HttpGet]
        public async Task<IActionResult> GetBestStoriesAsync([FromQuery][Range(1, 200)] int count = 1)
        {
            var stories = await _bestStoryService.GetBestStoriesAsync(count);

            if (stories == null)
            {
                return NoContent();
            }

            return Ok(stories);
        }
    }
}
