namespace Lab4.DependencyInjection.Exceptions;

public class CircularDependencyDetected : ArgumentException
{
    public CircularDependencyDetected(string? message, string? paramName, Type serviceType) : base(message, paramName)
    {
        ServiceType = serviceType;
    }
    
    public CircularDependencyDetected(string? message, Type serviceType) : base(message)
    {
        ServiceType = serviceType;
    }

    public Type ServiceType { get; }
}