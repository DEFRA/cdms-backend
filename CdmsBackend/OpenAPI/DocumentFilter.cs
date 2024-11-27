using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Cdms.Model.Ipaffs;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CdmsBackend.OpenAPI;

public class SchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var namingPolicy = JsonNamingPolicy.CamelCase;
        // Exclude BsonIgnore fields
        if (schema?.Properties == null || context.Type == null)
            return;


        foreach (var propertyInfo in context.Type.GetProperties())
        {
            var jsonAttr = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            var name = jsonAttr != null ? jsonAttr.Name : namingPolicy.ConvertName(propertyInfo.Name);
           
            // Add description
            var descAttr = propertyInfo.GetCustomAttribute<DescriptionAttribute>();
            if (descAttr is not null)
            {
                if (schema.Properties.TryGetValue(name, out var property))
                {
                    property.Description = descAttr.Description;
                }
            }

            // Exclude properties
            if (propertyInfo.GetCustomAttribute(typeof(BsonIgnoreAttribute)) != null ||
                false)//propertyInfo.GetCustomAttribute(typeof(ApiIgnoreAttribute)) != null)
            {
                if (schema.Properties.ContainsKey(name))
                    schema.Properties.Remove(name);
            }

            //Use the property name with camel casing rather than the jsonpropertyname
            if (schema.Properties.TryGetValue(name, out var existingProperty))
            {
                var newName = namingPolicy.ConvertName(propertyInfo.Name);
                if (schema.Properties.ContainsKey(name))
                {
                    schema.Properties.Remove(name);
                    schema.Properties.Add(newName, existingProperty);
                }
            }
        }


        if (context.Type.IsEnum)
        {
            var enumOpenApiStrings = new List<IOpenApiAny>();
            foreach (var enumValue in Enum.GetValues(context.Type))
            {
                enumOpenApiStrings.Add(new OpenApiString(enumValue.ToString()));
            }

            schema.Enum = enumOpenApiStrings;
        }
    }
}

public class NotificationResourceResponse : ResourceResponse<ImportNotification>
{
    
}

public class DocumentFilter : IDocumentFilter
{
    public DocumentFilter()
    {
        Thread.Sleep(20);
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        context.SchemaGenerator.GenerateSchema(typeof(NotificationResourceResponse), context.SchemaRepository);
        swaggerDoc.AddPath(
            path: "import-notifications",
            pathDescription: "Notification Operations",
            operationDescription: "Get Notifications",
            referenceId: "NotificationResourceResponse",
            tag: "Notifications");
    }

    
}