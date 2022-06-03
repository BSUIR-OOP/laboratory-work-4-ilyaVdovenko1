using System;
using System.Security.Authentication.ExtendedProtection;
using Lab4.DependencyInjection.Models;
using Lab4.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab4.Tests.Tests;

[TestClass]
public class SingletonDiTests
{
    [TestMethod]
    public void Test1_CreateInstanceWithoutImplementation()
    {
        var services = new DiServiceCollection();

        services.AddSingleton<StateModel>();

        var container = services.GenerateContainer();
        var serviceFirst = container.GetService<StateModel>();
        var serviceSecond = container.GetService<StateModel>();
        
        Console.WriteLine(serviceFirst.State);
        Console.WriteLine(serviceSecond.State);
        
        Assert.AreEqual(serviceFirst.State, serviceSecond.State);
        
        
    }
    
    [TestMethod]
    public void Test2_CreateInstanceWithImplementation()
    {
        var services = new DiServiceCollection();

        services.AddSingleton<StateModel>(new StateModel());

        var container = services.GenerateContainer();
        var serviceFirst = container.GetService<StateModel>();
        var serviceSecond = container.GetService<StateModel>();
        
        Console.WriteLine(serviceFirst.State);
        Console.WriteLine(serviceSecond.State);
        
        Assert.AreEqual(serviceFirst.State, serviceSecond.State);
        
        
    }
}