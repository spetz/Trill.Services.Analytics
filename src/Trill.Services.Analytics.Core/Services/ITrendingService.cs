using System.Collections.Generic;
using System.Threading.Tasks;
using Trill.Services.Analytics.Core.Models;

namespace Trill.Services.Analytics.Core.Services
{
    public interface ITrendingService
    {
        Task<IEnumerable<Story>> GetTopStoriesAsync();
        Task<IEnumerable<Tag>> GetTopTagsAsync();
        Task<IEnumerable<User>> GetTopUsersAsync();
    }
}