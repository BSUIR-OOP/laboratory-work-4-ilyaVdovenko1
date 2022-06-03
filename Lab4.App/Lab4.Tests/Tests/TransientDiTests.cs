using System;
using Lab4.DependencyInjection.Models;
using Lab4.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab4.Tests.Tests;

[TestClass]
public class TransientDiTests
{
    [TestMethod]
    public void Test1_CreateInstanceWithoutImplementation()
    {
        var services = new DiServiceCollection();

        services.AddTransient<StateModel>();

        var container = services.GenerateContainer();
        var serviceFirst = container.GetService<StateModel>();
        var serviceSecond = container.GetService<StateModel>();
        
        Console.WriteLine(serviceFirst.State);
        Console.WriteLine(serviceSecond.State);
        
        Assert.AreNotEqual(serviceFirst.State, serviceSecond.State);
    }
}