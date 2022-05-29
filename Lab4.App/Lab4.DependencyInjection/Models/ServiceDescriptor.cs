namespace Lab4.DependencyInjection.Models;

public class ServiceDescriptor
{
    public ServiceDescriptor(object implementation, ServiceLifetime lifetime)
    {
        ServiceType = implementation.GetType();
        Implementation = implementation;
        ServiceLifetime = lifetime;
        
    }
    
    

    public ServiceDescriptor(Type serviceType, ServiceLifetime lifetime)
    {
        ServiceType = serviceType;
        ServiceLifetime = lifetime;
        
    }
    
    public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
    {
        ServiceType = serviceType;
        ServiceLifetime = lifetime;
        ImplementationType = implementationType;

    }
    
    public Type ServiceType { get; }

    public Type? ImplementationType { get; }

    public object? Implementation { get; set; }
    
    public ServiceLifetime ServiceLifetime { get; }
    
}