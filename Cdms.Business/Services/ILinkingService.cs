using Cdms.Business.Pipelines.Matching;
using Cdms.Model.Alvs;
using Cdms.Model.Ipaffs;

namespace Cdms.Business.Services;

public interface ILinkingService
{
    Task<MatchContext> Link(AlvsClearanceRequest clearanceRequest);
    Task<MatchContext> Link(ImportNotification notification);
}