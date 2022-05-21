using System.Reflection;

namespace Lab3App.Domain.Models;

public class RecordSnapshotConfiguration
{
    public RecordSnapshotConfiguration(Type baseClassType)
    {
        var parent = baseClassType;
        var types = Assembly.GetExecutingAssembly().GetTypes();
        var inheritingTypes = types.Where(t => parent.IsAssignableFrom(t));

        this.ClassHierarchy = new Dictionary<string, Type>();
        foreach (var type in inheritingTypes)
        {
            this.ClassHierarchy.Add(type.Name, type);
        }
    }
    public Dictionary<string, Type> ClassHierarchy { get; set; }
}