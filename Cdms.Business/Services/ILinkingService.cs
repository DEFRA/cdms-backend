using Cdms.Business.Pipelines.Matching;
using Cdms.Model.Alvs;

namespace Cdms.Business.Services;

public interface ILinkingService
{
    Task<LinkResult> Link(LinkContext linkContext);
}