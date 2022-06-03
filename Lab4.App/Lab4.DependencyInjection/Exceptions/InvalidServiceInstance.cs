namespace Lab4.DependencyInjection.Exceptions;

public class InvalidServiceInstance : ArgumentException
{
    public InvalidServiceInstance(string? message, string? paramName, Type serviceType) : base(message, paramName)
    {
        ServiceType = serviceType;
    }
    
    public InvalidServiceInstance(string? message, Type serviceType) : base(message)
    {
        ServiceType = serviceType;
    }

    public Type ServiceType { get; }
}