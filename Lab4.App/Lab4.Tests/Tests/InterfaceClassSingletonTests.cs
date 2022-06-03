using System;
using Lab4.DependencyInjection.Models;
using Lab4.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab4.Tests.Tests;

[TestClass]
public class InterfaceClassSingletonTests
{
    [TestMethod]
    public void Test1_CreateInstanceWithoutImplementation()
    {
        var services = new DiServiceCollection();

        services.AddSingleton<IState, StateModel>();

        var container = services.GenerateContainer();
        var serviceFirst = container.GetService<IState>();
        var serviceSecond = container.GetService<IState>();
        
        Console.WriteLine(serviceFirst.State);
        Console.WriteLine(serviceSecond.State);
        
        Assert.AreEqual(serviceFirst.State, serviceSecond.State);
        
        
    }
    
    [TestMethod]
    public void Test2_CreateInstanceWithoutImplementationWithDefault()
    {
        var services = new DiServiceCollection();

        services.AddSingleton<IState, DefaultState>();

        var container = services.GenerateContainer();
        var serviceFirst = container.GetService<IState>();


        Assert.AreEqual(default, serviceFirst.State);
        
        
    }
}