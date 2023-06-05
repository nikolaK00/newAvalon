﻿using NewAvalon.Domain.Abstractions;
using System;

namespace NewAvalon.Order.Domain.EntityIdentifiers
{
    public sealed record ProductId(Guid Value) : IEntityId;
}
