using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trill.Services.Analytics.Core.Models;
using Trill.Services.Analytics.Core.Services;

namespace Trill.Services.Analytics.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrendingController : ControllerBase
    {
        private readonly ITrendingService _trendingService;

        public TrendingController(ITrendingService trendingService)
        {
            _trendingService = trendingService;
        }

        [HttpGet("stories")]
        public async Task<ActionResult<IEnumerable<Story>>> GetStories()
            => Ok(await _trendingService.GetTopStoriesAsync());

        [HttpGet("tags")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
            => Ok(await _trendingService.GetTopTagsAsync());

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
            => Ok(await _trendingService.GetTopUsersAsync());
    }
}