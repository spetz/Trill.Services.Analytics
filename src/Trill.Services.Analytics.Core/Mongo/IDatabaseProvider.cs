using MongoDB.Driver;
using Trill.Services.Analytics.Core.Models;
using Tag = Trill.Services.Analytics.Core.Models.Tag;

namespace Trill.Services.Analytics.Core.Mongo
{
    public interface IDatabaseProvider
    {
        IMongoCollection<Story> Stories { get; }
        IMongoCollection<Tag> Tags { get; }
        IMongoCollection<User> Users { get; }
    }
}