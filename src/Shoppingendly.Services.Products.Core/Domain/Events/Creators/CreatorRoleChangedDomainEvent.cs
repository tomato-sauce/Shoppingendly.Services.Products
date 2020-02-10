﻿using Shoppingendly.Services.Products.Core.Domain.Base.DomainEvents;
using Shoppingendly.Services.Products.Core.Domain.ValueObjects;

namespace Shoppingendly.Services.Products.Core.Domain.Events.Creators
{
    public class CreatorRoleChangedDomainEvent : DomainEventBase
    {
        public CreatorId CreatorId { get; }
        public Role Role { get; }

        public CreatorRoleChangedDomainEvent(CreatorId creatorId, Role role)
        {
            CreatorId = creatorId;
            Role = role;
        }
    }
}