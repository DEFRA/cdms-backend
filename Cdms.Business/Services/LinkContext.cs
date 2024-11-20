using Cdms.Model;
using Cdms.Model.Ipaffs;

namespace Cdms.Business.Services;

public abstract class LinkContext
{
    public static MovementLinkContext ForMovement(Movement movement)
    {
        return new MovementLinkContext(movement);
    }

    public static ImportNotificationLinkContext ForImportNotification(ImportNotification importNotification)
    {
        return new ImportNotificationLinkContext(importNotification);
    }
}