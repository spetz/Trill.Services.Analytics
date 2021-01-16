using MongoDB.Driver;
using Trill.Services.Analytics.Core.Models;
using Tag = Trill.Services.Analytics.Core.Models.Tag;

namespace Trill.Services.Analytics.Core.Mongo
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private readonly IMongoDatabase _database;

        public DatabaseProvider(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<Story> Stories => _database.GetCollection<Story>("stories");
        public IMongoCollection<Tag> Tags => _database.GetCollection<Tag>("tags");
        public IMongoCollection<User> Users => _database.GetCollection<User>("users");
    }
}