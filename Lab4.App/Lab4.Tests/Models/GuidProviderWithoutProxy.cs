using System;

namespace Lab4.Tests.Models;

public class GuidProviderWithoutProxy : IGuidProvider
{
    public Guid Guid { get; set; } = Guid.NewGuid();
}