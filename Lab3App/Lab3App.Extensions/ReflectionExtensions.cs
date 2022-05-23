using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Lab3App.Extensions;

public static class ReflectionExtensions
{
    public static IEnumerable<Type> GetParentClasses(this Type childType, Type baseClass)
    {
        if (!baseClass.IsAssignableFrom(childType))
        {
            throw new ArgumentException($"Base type {baseClass} is not a parent of child type: {childType}.");
        }
        
        var type = childType;
        var typeHierarchy = new List<Type>();
        while (type.BaseType != null && type != baseClass)
        {

            if (type.IsClass)
                if (type.BaseType == typeof(object))
                    break;
            typeHierarchy.Add(type);
            type = type.BaseType;
        }
                
                    
        

        return typeHierarchy;
    }
    
    public static IEnumerable<PropertyInfo> GetPropertiesOfCollection(this IEnumerable<Type> types)
    {
        return types.SelectMany(type => type.GetClassProperties()).ToList();
    }

    public static Delegate CreateGetter(PropertyInfo property)
    {
        var objectParameter = Expression.Parameter(property.DeclaringType!, "o");
        var delegateType = typeof(Func<,>).MakeGenericType(property.DeclaringType!, property.PropertyType);
        var lambda = Expression.Lambda(delegateType, Expression.Property(objectParameter, property.Name), objectParameter);
        return lambda.Compile();
    }

    public static Delegate CreateSetter(PropertyInfo property)
    {
        var objectParameter = Expression.Parameter(property.DeclaringType!, "o");
        var valueParameter = Expression.Parameter(property.PropertyType, "value");
        var delegateType = typeof(Action<,>).MakeGenericType(property.DeclaringType!, property.PropertyType);
        var lambda = Expression.Lambda(delegateType, Expression.Assign(Expression.Property(objectParameter, property.Name), valueParameter), objectParameter, valueParameter);
        return lambda.Compile();
    }
    
    public static IEnumerable<Type> TypesImplementingInterface(this Type desiredType)
    {
        return AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => desiredType.IsAssignableFrom(type));
    }

    public static IEnumerable<PropertyInfo> GetClassProperties(this Type type)
    {
        var properties = new List<PropertyInfo>();
        foreach (var property in type.GetProperties())
        {
            if (property.PropertyType != typeof(string) && property.PropertyType.IsClass)
            {
                properties.AddRange(property.PropertyType.GetClassProperties());
                continue;
            }
            properties.Add(property);
        }

        return properties;

    }

    public static IEnumerable<PropertyInfo> GetClassPropertiesWithoutDiveIn(this Type type)
    {
        return type.GetProperties().ToList();
    }

    public static object FillStringPropertiesRecursively(this Type baseType, object instance, Dictionary<string, object> data)
    {
        foreach (var propertyData in baseType.GetClassPropertiesWithoutDiveIn())
        {
            if (propertyData.CanWrite)
            {
                if (propertyData.PropertyType == typeof(string))
                {
                    
                    propertyData.SetValue(instance, data[propertyData.Name]);
                }
                else
                {
                    var prototype = Activator.CreateInstance(propertyData.PropertyType);
                    

                    
                    var filled = FillStringPropertiesRecursively(propertyData.PropertyType, prototype, data);
                    propertyData.SetValue(instance, filled);
                }

                
                
            }
        }

        return instance;
    }
}