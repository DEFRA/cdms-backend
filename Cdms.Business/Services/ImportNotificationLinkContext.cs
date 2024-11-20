using Cdms.Model.Ipaffs;

namespace Cdms.Business.Services;

public class ImportNotificationLinkContext(ImportNotification importNotification) : LinkContext
{
    public ImportNotification ImportNotification { get; } = importNotification;
}