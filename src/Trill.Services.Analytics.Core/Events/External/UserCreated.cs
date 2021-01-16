using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Trill.Services.Analytics.Core.Events.External
{
    [Message("users")]
    public class UserCreated : IEvent
    {
        public Guid UserId { get; }
        public string Name { get; }

        public UserCreated(Guid userId, string name)
        {
            UserId = userId;
            Name = name;
        }
    }
}