using JsonApiDotNetCore.Resources.Annotations;

namespace Cdms.Model.Relationships;

public sealed class ResourceLink
{
    [Attr]
    public string Self { get; set; } = default!;
}