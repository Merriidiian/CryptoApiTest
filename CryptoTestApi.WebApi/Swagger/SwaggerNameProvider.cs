namespace CryptoTestApi.Swagger;

public static class SwaggerNameProvider
{
    public static string GetSwaggerDisplayedName(Type type)
    {
        return BuildDisplayedTypeName(type);
    }

    private static string BuildDisplayedTypeName(Type type)
    {
        var prefix = string.Empty;

        if (type.DeclaringType is not null)
        {
            prefix = BuildDisplayedTypeName(type.DeclaringType) + "_";
        }

        if (!type.IsGenericType) return prefix + type.Name;

        var cleanTypeName = type.Name[.. ^2]; // допускаем, что больше 9 generic-параметров в типе не будет
        var genericArguments = string.Join("And", type.GenericTypeArguments.Select(BuildDisplayedTypeName));

        return $"{prefix}{cleanTypeName}Of{genericArguments}";

    }
}