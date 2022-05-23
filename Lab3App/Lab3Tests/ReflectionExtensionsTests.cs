using System;
using System.Collections.Generic;
using Lab3App.Application.Extensions;
using Lab3App.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab3Tests;

[TestClass]
public class ReflectionExtensionsTests
{
    [TestMethod]
    public void GetParentClassesTest_WorkerBase_Developer()
    {
        //init
        var baseType = typeof(WorkerBase);
        var childType = typeof(Developer);
        var expected = new List<Type>()
        {
            typeof(Developer),
            typeof(OfficeWorker),
            
        };
        //act
        var actual = childType.GetParentClasses(baseType);
        
        //assert
        CollectionAssert.AreEqual(expected, new List<Type>(actual));
    }
}