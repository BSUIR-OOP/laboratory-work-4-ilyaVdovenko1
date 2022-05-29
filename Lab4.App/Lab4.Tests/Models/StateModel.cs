using System;

namespace Lab4.Tests.Models;

public class StateModel : IState
{
    public Guid State { get; set; } = Guid.NewGuid();
}