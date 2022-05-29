using System;

namespace Lab4.Tests.Models;

public class CurrentState : IState
{
    public CurrentState(IGuidProvider provider)
    {
        State = provider.Guid;
    }

    public Guid State { get; set; }
}