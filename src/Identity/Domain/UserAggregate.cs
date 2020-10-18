using System;
using System.Collections.Generic;

namespace Identity.Domain
{
    public class UserAggregate : Aggregate<UserGuid>
    {
        public Fullname Fullname { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }

        public void Create(Fullname fullname, Email email, Password password)
        {
            var @event = new UserCreatedEvent(
                Id,
                fullname.Firstname,
                fullname.Lastname,
                email.Value,
                password.Value);
            
            Emit(@event);
        }

        public void Handle(UserCreatedEvent @event)
        {
            Fullname = new Fullname(@event.Firstname, @event.Lastname);
            Email = new Email(@event.Email);
            Password = new Password(@event.Password, false);
        }

        #region Constructors
        public UserAggregate(TenantId tenantId) : base(tenantId)
        {
        }

        public UserAggregate(TenantId tenantId, Guid id) : base(tenantId, id)
        {
        }

        public UserAggregate(IAggregateId<Guid> id) : base(id)
        {
        }

        public UserAggregate(string id) : base(id)
        {
        }

        public UserAggregate(IReadOnlyCollection<IDomainEvent> events) : base(events)
        {
        }
        #endregion

    }
}