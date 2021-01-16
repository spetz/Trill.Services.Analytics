using System.Threading.Tasks;
using Convey.CQRS.Events;
using Trill.Services.Analytics.Core.Services;

namespace Trill.Services.Analytics.Core.Events.External.Handlers
{
    internal sealed class UserUnfollowedHandler : IEventHandler<UserUnfollowed>
    {
        private readonly IUsersService _usersService;

        public UserUnfollowedHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }
        
        public async Task HandleAsync(UserUnfollowed @event)
        {
            await _usersService.DecrementFollowersCountAsync(@event.FollowerId, @event.FolloweeId);
        }
    }
}