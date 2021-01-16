using System.Collections.Generic;
using System.Threading.Tasks;
using Trill.Services.Analytics.Core.Models;
using Trill.Services.Analytics.Core.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Tag = Trill.Services.Analytics.Core.Models.Tag;

namespace Trill.Services.Analytics.Core.Services
{
    internal class TrendingService : ITrendingService
    {
        private readonly IDatabaseProvider _databaseProvider;

        public TrendingService(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public async Task<IEnumerable<Story>> GetTopStoriesAsync()
            => await _databaseProvider.Stories.AsQueryable()
                .OrderByDescending(x => x.TotalRate)
                .Take(10)
                .ToListAsync();

        public async Task<IEnumerable<Tag>> GetTopTagsAsync()
            => await _databaseProvider.Tags.AsQueryable()
                .OrderByDescending(x => x.OccurenceCount)
                .Take(10)
                .ToListAsync();

        public async Task<IEnumerable<User>> GetTopUsersAsync()
            => await _databaseProvider.Users.AsQueryable()
                .OrderByDescending(x => x.FollowersCount)
                .ThenByDescending(x => x.StoriesCount)
                .Take(10)
                .ToListAsync();
    }
}