using System;
using System.Threading.Tasks;
using Trill.Services.Analytics.Core.Models;

namespace Trill.Services.Analytics.Core.Services
{
    public interface IUsersService
    {
        Task AddAsync(User user);
        Task IncrementStoriesCountAsync(Guid userId);
        Task IncrementFollowersCountAsync(Guid followerId, Guid followeeId);
        Task DecrementFollowersCountAsync(Guid followerId, Guid followeeId);
    }
}