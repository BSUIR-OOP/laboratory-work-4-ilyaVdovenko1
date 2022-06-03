using System;
using Lab4.DependencyInjection.Exceptions;
using Lab4.DependencyInjection.Models;
using Lab4.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab4.Tests.Tests;

[TestClass]
public class CtorInjectionTests
{
    [TestMethod]
    [ExpectedException(typeof(CircularDependencyDetected))]
    public void Test1_CreateInstanceWithoutImplementation()
    {
        var services = new DiServiceCollection();

        services.AddSingleton<IGuidProvider, GuidProvider>();
        services.AddSingleton<IProxyService, ProxyClass>();
        services.AddSingleton<IState, CurrentState>();

        var container = services.GenerateContainer();
        var serviceFirst = container.GetService<IState>();
        var serviceSecond = container.GetService<IState>();
        
        Console.WriteLine(serviceFirst.State);
        Console.WriteLine(serviceSecond.State);
        
        Assert.AreEqual(serviceFirst.State, serviceSecond.State);
        
        
    }
    
    [TestMethod]
    public void Test2_CreateInstanceWithoutImplementationWithTransient()
    {
        var services = new DiServiceCollection();

        services.AddTransient<IGuidProvider, GuidProviderWithoutProxy>();
        services.AddTransient<IState, CurrentState>();

        var container = services.GenerateContainer();
        var serviceFirst = container.GetService<IState>();
        var serviceSecond = container.GetService<IState>();
        
        Console.WriteLine(serviceFirst.State);
        Console.WriteLine(serviceSecond.State);
        
        Assert.AreNotEqual(serviceFirst.State, serviceSecond.State);
        
        
    }
    
    [TestMethod]
    public void Test3_CreateInstanceWithoutImplementationWithSingletonAndTransient()
    {
        var services = new DiServiceCollection();

        services.AddSingleton<IGuidProvider, GuidProviderWithoutProxy>();
        services.AddTransient<IState, CurrentState>();

        var container = services.GenerateContainer();
        var serviceFirst = container.GetService<IState>();
        var serviceSecond = container.GetService<IState>();
        
        Console.WriteLine(serviceFirst.State);
        Console.WriteLine(serviceSecond.State);
        
        Assert.AreEqual(serviceFirst.State, serviceSecond.State);
        
        
    }
    
}