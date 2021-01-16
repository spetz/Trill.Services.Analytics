using System.Threading.Tasks;
using Trill.Services.Analytics.Core.Models;

namespace Trill.Services.Analytics.Core.Services
{
    public interface IStoriesService
    {
        Task AddAsync(Story story);
        Task SetTotalRateAsync(long storyId, int totalRate);
    }
}