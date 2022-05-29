using System;

namespace Lab4.Tests.Models;

public class GuidProvider : IGuidProvider
{
    public GuidProvider(IProxyService service)
    {
        
    }
    public Guid Guid { get; set; } = Guid.NewGuid();
}