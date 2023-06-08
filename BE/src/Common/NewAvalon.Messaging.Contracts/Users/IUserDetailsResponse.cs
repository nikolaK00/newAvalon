﻿using System;
using System.Collections.Generic;

namespace NewAvalon.Messaging.Contracts.Users
{
    public interface IUserDetailsResponse
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> Roles { get; set; }
    }
}
