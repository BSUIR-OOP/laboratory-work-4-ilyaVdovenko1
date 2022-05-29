namespace Lab4.DependencyInjection.Exceptions;

public class ServiceNotFoundException : ArgumentException
{
    public ServiceNotFoundException(string? message, string? paramName, Type serviceType) : base(message, paramName)
    {
        ServiceType = serviceType;
    }
    
    public ServiceNotFoundException(string? message, Type serviceType) : base(message)
    {
        ServiceType = serviceType;
    }

    public Type ServiceType { get; }

    
}