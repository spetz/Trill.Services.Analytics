using System.Threading.Tasks;
using Trill.Services.Analytics.Core.Models;

namespace Trill.Services.Analytics.Core.Services
{
    public interface ITagsService
    {
        Task<bool> TryAddAsync(Tag tag);
        Task IncrementOccurrencesCountAsync(string tag);
    }
}