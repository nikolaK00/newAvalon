﻿using NewAvalon.Messaging.Contracts.Users;
using System;
using System.Collections.Generic;

namespace NewAvalon.UserAdministration.Business.Contracts.Users
{
    internal sealed class UserDetailsResponse : IUserDetailsResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> Roles { get; set; }
    }
}
