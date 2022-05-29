using System;

namespace Lab4.Tests.Models;

public class DefaultState : IState
{
    public Guid State { get; set; } = default(Guid);
}