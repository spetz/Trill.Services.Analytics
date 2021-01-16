using System.Threading.Tasks;
using MongoDB.Driver;
using Trill.Services.Analytics.Core.Models;
using Trill.Services.Analytics.Core.Mongo;

namespace Trill.Services.Analytics.Core.Services
{
    internal class StoriesService : IStoriesService
    {
        private readonly IDatabaseProvider _databaseProvider;

        public StoriesService(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }
        
        public async Task AddAsync(Story story)
        {
            await _databaseProvider.Stories.InsertOneAsync(story);
        }
        public async Task SetTotalRateAsync(long storyId, int totalRate)
        {
            var builder = Builders<Story>.Filter;
            var filter = builder.Eq(x => x.Id, storyId);
            var update = Builders<Story>.Update.Set(s => s.TotalRate, totalRate);
            await _databaseProvider.Stories.FindOneAndUpdateAsync(filter, update);
        }
    }
}