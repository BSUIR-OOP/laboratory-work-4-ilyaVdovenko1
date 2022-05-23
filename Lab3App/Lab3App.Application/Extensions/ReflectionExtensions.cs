using System.Linq.Expressions;
using System.Reflection;

namespace Lab3App.Application.Extensions;

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
        return types.SelectMany(type => type.GetProperties()).ToList();
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
}