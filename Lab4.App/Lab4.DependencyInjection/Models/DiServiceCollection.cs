namespace Lab4.DependencyInjection.Models;

public class DiServiceCollection
{
    private List<ServiceDescriptor> serviceDescriptors = new List<ServiceDescriptor>();
    public void AddSingleton<T>()
    {
        serviceDescriptors.Add(new ServiceDescriptor(typeof(T), ServiceLifetime.Singleton));
    }
    
    public void AddSingleton<TService, TImplementation>() where TImplementation : TService
    {
        serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Singleton));
    }

    public void AddSingleton<TService>(TService implementation)
    {
        serviceDescriptors.Add(new ServiceDescriptor(implementation ?? throw new ArgumentNullException(nameof(implementation)), ServiceLifetime.Singleton));
    }
    
    public void AddTransient<TService>()
    {
        serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), ServiceLifetime.Transient));
    }
    
    public void AddTransient<TService, TImplementation>() where TImplementation : TService
    {
        serviceDescriptors.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient));
    }

    public DiContainer GenerateContainer()
    {
        return new DiContainer(serviceDescriptors);
    }

    
}