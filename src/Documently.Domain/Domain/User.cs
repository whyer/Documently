using System;
using CommonDomain.Core;
using Documently.Domain.Events;

namespace Documently.Domain.Domain
{
    public class User : AggregateBase
    {
        //private User(Guid id, UserName UserName, Address address, PhoneNumber phoneNumber)
        //{
        //    RaiseEvent(new UserCreatedEvent(id, UserName.Name, address.Street, address.StreetNumber, address.PostalCode,
        //                                        address.City, phoneNumber.Number));
        //}

        public User()
        {
        }


        //public static User CreateNew(Guid id, UserName UserName, Address address, PhoneNumber phoneNumber)
        //{
        //    return new User(id, UserName, address, phoneNumber);
        //}

        //Domain-Eventhandlers
        //private void Apply(UserCreatedEvent @event)
        //{
        //    Id = @event.AggregateId;
        //    // we don't need to keep any other state here.
        //}
    }
}