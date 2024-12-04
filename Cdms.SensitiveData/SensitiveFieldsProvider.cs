using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cdms.SensitiveData;

public static class SensitiveFieldsProvider
{
    private static Dictionary<Type, List<string>> cache = new();
    private static readonly object cacheLock = new ();
    public static List<string> Get<T>()
    {
        lock (cacheLock)
        {
            if (cache.TryGetValue(typeof(T), out var value))
            {
                return value;
            }

            var type = typeof(T);

            var list = GetSensitiveFields(string.Empty, type);
            cache.Add(typeof(T), list);
            return list;
        }

       
    }

    public static List<string> Get(Type type)
    {
        lock (cacheLock)
        {
            if (cache.TryGetValue(type, out var value))
            {
                return value;
            }

            var list = GetSensitiveFields(string.Empty, type);
            cache.Add(type, list);
            return list;
        }


    }

    private static List<string> GetSensitiveFields(string root, Type type)
    {
        var namingPolicy = JsonNamingPolicy.CamelCase;
        var list = new List<string>();
        foreach (var property in type.GetProperties())
        {
            var jsonPropertyNameAttribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();
            var propertyName = jsonPropertyNameAttribute != null ? jsonPropertyNameAttribute.Name : property.Name;
            string currentPath;
            currentPath = string.IsNullOrEmpty(root) ? $"{namingPolicy.ConvertName(propertyName)}" : $"{namingPolicy.ConvertName(root)}.{namingPolicy.ConvertName(propertyName)}";

            if (property.CustomAttributes.Any(x => x.AttributeType == typeof(SensitiveDataAttribute)))
            {
                list.Add(currentPath);
            }
            else
            {
                Type elementType = GetElementType(property)!;

                if (elementType != null && elementType.Namespace != "System")
                {
                    list.AddRange(GetSensitiveFields($"{currentPath}", elementType!));
                }
            }
        }

        return list;
    }

    private static Type? GetElementType(PropertyInfo property)
    {
        if (property.PropertyType.IsArray)
        {
            return property.PropertyType.GetElementType()!;
        }
        else if (property.PropertyType.IsGenericType)
        {
            return property.PropertyType.GetGenericArguments()[0];

        }
        else if (property.PropertyType.IsClass)
        {
            return property.PropertyType;

        }

        return default;
    }
}