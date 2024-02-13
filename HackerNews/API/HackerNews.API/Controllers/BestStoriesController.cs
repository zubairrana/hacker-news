using HackerNews.BusinessLogic.Contracts;
using HackerNews.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackerNews.API.Controllers
{
    [Route("api/best-stories")]
    [ApiController]
    public class BestStoriesController : ControllerBase
    {
        private readonly IBestStoryService _bestStoryService;
        
        public BestStoriesController(IBestStoryService bestStoryService) 
        {
            _bestStoryService = bestStoryService;
            
        }

        /// <summary>
        /// Returns best stories
        /// </summary>
        /// <param name="count">Minimun Value: 1 and Max Value: 200</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BestStory>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBestStoriesAsync([FromQuery][Range(1, 200)][Required][DefaultValue(1)] int count)
        {
            var stories = await _bestStoryService.GetBestStoriesAsync(count);

            if (stories == null || !stories.Any())
            {
                return NoContent();
            }

            return Ok(stories);
        }
    }
}
