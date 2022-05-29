using System;
using Lab4.DependencyInjection.Models;
using Lab4.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab4.Tests.Tests;

[TestClass]
public class InterfaceClassTransientTests
{
    [TestMethod]
    public void Test1_CreateInstanceWithoutImplementation()
    {
        var services = new DiServiceCollection();

        services.AddTransient<IState, StateModel>();

        var container = services.GenerateContainer();
        var serviceFirst = container.GetService<IState>();
        var serviceSecond = container.GetService<IState>();
        
        Console.WriteLine(serviceFirst.State);
        Console.WriteLine(serviceSecond.State);
        
        Assert.AreNotEqual(serviceFirst.State, serviceSecond.State);
    }
    
    [TestMethod]
    public void Test2_CreateInstanceWithoutImplementationWithDefaultValue()
    {
        var services = new DiServiceCollection();

        services.AddTransient<IState, DefaultState>();

        var container = services.GenerateContainer();
        var serviceFirst = container.GetService<IState>();

        Console.WriteLine(serviceFirst.State);

        Assert.AreEqual(default, serviceFirst.State);
    }
}