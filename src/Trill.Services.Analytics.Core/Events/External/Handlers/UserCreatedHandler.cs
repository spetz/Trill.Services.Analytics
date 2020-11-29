using System.Threading.Tasks;
using Convey.CQRS.Events;
using Trill.Services.Analytics.Core.Models;
using Trill.Services.Analytics.Core.Services;

namespace Trill.Services.Analytics.Core.Events.External.Handlers
{
    internal sealed class UserCreatedHandler : IEventHandler<UserCreated>
    {
        private readonly IUsersService _usersService;

        public UserCreatedHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task HandleAsync(UserCreated @event)
        {
            await _usersService.AddAsync(new User
            {
                Id = @event.UserId,
                Name = @event.Name
            });
        }
    }
}