namespace Lab4.DependencyInjection.Models;

public class ActualType
{
    public ActualType(Type type, bool wasVisited)
    {
        Type = type;
        WasVisited = wasVisited;
    }

    public Type Type { get; }
    public bool WasVisited { get; }
}