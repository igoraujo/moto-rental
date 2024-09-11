using Dapper;

namespace MotoRental.Borders.Helpers;

public static class DapperHelper
{
    public static DynamicParameters ConvertToLowercaseParameters<T>(this T obj)
    {
        var parameters = new DynamicParameters();
        
        var properties = typeof(T).GetProperties();
        
        foreach (var prop in properties)
        {
            var propName = prop.Name.ToLower(); 
            var propValue = prop.GetValue(obj); 
            parameters.Add($"@{propName}", propValue);
        }

        return parameters;
    }
    
    public static int GetIdValue<T>(this T content)
    {
        var id = 0;
        var type = content?.GetType();
        var property = type?.GetProperty("Id");
        if (property is not null)
        {
            id = (int)property.GetValue(content);
        }
        return id;
    }
    
    public static T SetSourceValueIfContentValueNull<T>(T content, T source)
    {
        var typeContent = content.GetType();

        foreach (var property in typeContent.GetProperties())
        {
            var valueContent = property.GetValue(content);
            var valueSource = property.GetValue(source);

            if (valueContent is null)
            {
                property.SetValue(content, valueSource);
            }
        }

        return content;
    }
}