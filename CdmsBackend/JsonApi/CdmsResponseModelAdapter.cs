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
            : new List<ResourceObject>() { document.Data.SingleValue };


        foreach (var resourceObject in listOfResourceObjects)
        {
            if (resourceObject.Attributes.TryGetValue("relationships", out var value))
            {
                var relationships = (value as ITdmRelationships).GetRelationshipObjects();
                resourceObject.Relationships = new Dictionary<string, RelationshipObject?>();

                foreach (var relationship in relationships)
                {
                    var list = relationship.Item2.Data.Select(item => new ResourceIdentifierObject()
                        {
                            Type = item.Type,
                            Id = item.Id,
                            //Lid = item.Id, 
                            Meta = new Dictionary<string, object?>()
                            {
                                { "matched", item.Matched },
                                { "sourceItem", item.SourceItem },
                                { "destinationItem", item.DestinationItem },
                                { "matchingLevel", item.MatchingLevel },
                                { "self", item.Links?.Self }
                            },
                        })
                        .ToList();

                    resourceObject.Relationships.Add(relationship.Item1,
                        new RelationshipObject()
                        {
                            Meta = new Dictionary<string, object?>() { { "matched", relationship.Item2.Matched } },
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


        return document;
    }
}