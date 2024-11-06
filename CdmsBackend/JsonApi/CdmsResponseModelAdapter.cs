using Cdms.Model.Relationships;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Middleware;
using JsonApiDotNetCore.Queries;
using JsonApiDotNetCore.QueryStrings;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Serialization.Objects;
using JsonApiDotNetCore.Serialization.Response;
using RelationshipLinks = JsonApiDotNetCore.Serialization.Objects.RelationshipLinks;

namespace CdmsBackend.JsonApi;

public class CdmsResponseModelAdapter(
    IJsonApiRequest request,
    IJsonApiOptions options,
    ILinkBuilder linkBuilder,
    IMetaBuilder metaBuilder,
    IResourceDefinitionAccessor resourceDefinitionAccessor,
    IEvaluatedIncludeCache evaluatedIncludeCache,
    ISparseFieldSetCache sparseFieldSetCache,
    IRequestQueryStringAccessor requestQueryStringAccessor)
    : IResponseModelAdapter
{
    private readonly IResponseModelAdapter inner = new ResponseModelAdapter(request, options, linkBuilder, metaBuilder,
        resourceDefinitionAccessor,
        evaluatedIncludeCache, sparseFieldSetCache, requestQueryStringAccessor);

    public Document Convert(object? model)
    {
        var document = inner.Convert(model);
        if (document.Data.Value is null)
        {
            return document;
        }

        var listOfResourceObjects = document.Data.ManyValue is not null
            ? document.Data.ManyValue.ToList()
            : [document.Data.SingleValue];


        foreach (var resourceObject in listOfResourceObjects)
        {
            if (resourceObject.Attributes.TryGetValue("relationships", out var value))
            {
                ProcessRelationships(value, resourceObject);
            }
        }


        return document;
    }

    private static void ProcessRelationships(object? value, ResourceObject resourceObject)
    {
        var relationships = (value as ITdmRelationships).GetRelationshipObjects();
        resourceObject.Relationships = new Dictionary<string, RelationshipObject?>();

        foreach (var relationship in relationships)
        {
            var list = relationship.Item2.Data.Select(item =>
                    new ResourceIdentifierObject()
                    {
                        Type = item.Type, Id = item.Id, Meta = item.ToDictionary(),
                    })
                .ToList();


            var meta = new Dictionary<string, object?>();

            if (relationship.Item2.Matched.HasValue)
            {
                meta.Add("matched", relationship.Item2.Matched);
            }

            resourceObject.Relationships.Add(relationship.Item1,
                new RelationshipObject()
                {
                    Meta = meta,
                    Links = new RelationshipLinks()
                    {
                        Self = relationship.Item2.Links?.Self,
                        Related = relationship.Item2?.Links?.Related
                    },
                    Data = new SingleOrManyData<ResourceIdentifierObject>(list)
                });
        }

        resourceObject.Attributes.Remove("relationships");
    }
}