using Lab4.DependencyInjection.Exceptions;

namespace Lab4.DependencyInjection.Models;

public class DiContainer
{
    public Dictionary<Type, ServiceDescriptor> ServiceDescriptors { get; }

    public HashSet<Type> VisitedTypes;
    public DiContainer(IEnumerable<ServiceDescriptor> serviceDescriptors)
    {
        VisitedTypes = new HashSet<Type>();
        ServiceDescriptors = serviceDescriptors.ToDictionary(desc => desc.ServiceType);
    }
    
    public T GetService<T>()
    {
        VisitedTypes = new HashSet<Type>();
        return (T)GetService(typeof(T));
    }
    
    private object GetService(Type type)
    {
        
        
        if (!ServiceDescriptors.ContainsKey(type))
        {
            const string message = "Service with this type was not found";
            throw new ServiceNotFoundException(message, type);
        }
        
        if (VisitedTypes.Contains(type))
        {
            throw new CircularDependencyDetected("Circular dependency detected.", type);
        }

        VisitedTypes.Add(type);
        var descriptor = ServiceDescriptors[type];
        
        var implementation = descriptor.Implementation;
        if (implementation != null)
        {
            return implementation;
        }

        var actualType = descriptor.ImplementationType ?? descriptor.ServiceType;
        
       
        if (actualType.IsAbstract || actualType.IsInterface)
        {
            throw new InvalidServiceInstance("Can not create instance of abstract class or interface.", actualType);
        }

        var constructorInfo = actualType.GetConstructors().First();
        
        
        var parameters = constructorInfo.GetParameters().Select(x => GetService(x.ParameterType)).ToArray();        
        
        implementation = Activator.CreateInstance(actualType, parameters) ?? throw new InvalidOperationException("Invalid cast");
        if (descriptor.ServiceLifetime is ServiceLifetime.Singleton)
        {
            descriptor.Implementation = implementation;
        }

        return implementation;
    }
}