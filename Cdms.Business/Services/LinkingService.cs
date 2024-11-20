using Cdms.Backend.Data;
using Cdms.Model;
using Cdms.Model.Ipaffs;
using Cdms.Model.Relationships;
using Microsoft.EntityFrameworkCore;

namespace Cdms.Business.Services;

public class LinkingService(IMongoDbContext dbContext) : ILinkingService
{
    public async Task<LinkResult> Link(LinkContext linkContext)
    {
        LinkResult result;
        switch (linkContext)
        {
            case MovementLinkContext movementLinkContext:
                //TODO: Check if should attempt link
                result = await FindMovementLinks(movementLinkContext.Movement);
                break;
            case ImportNotificationLinkContext notificationLinkContext:
                //TODO: Check if should attempt link
                result = await FindImportNotificationLinks(notificationLinkContext.ImportNotification);
                break;
            default: throw new ArgumentException("context type not supported");
        }

        foreach (var notification in result.Notifications)
        {
            foreach (var movement in result.Movements)
            {
                notification.AddRelationship(new TdmRelationshipObject()
                {
                    Links = RelationshipLinks.CreateForNotification(notification),
                    Data = [RelationshipDataItem.CreateFromMovement(notification, movement, notification._MatchReference.ToString())]
                });

                movement.AddRelationship(new TdmRelationshipObject()
                {
                    Links = RelationshipLinks.CreateForMovement(movement),
                    Data = [RelationshipDataItem.CreateFromNotification(notification, movement, notification._MatchReference.ToString())]
                });

                await dbContext.Movements.Update(movement, movement._Etag);
                await dbContext.Notifications.Update(notification, notification._Etag);
            }
        }

        return result;
    }


    //TODO: Could this be from the audit, or pass in it within the context
    //private void ShouldLink(Movement movement)
    //{
    //    if (movement.AuditEntries.Last().Status == "Created")
    //    {

    //        //is new so attempt link
    //    }
    //    else if (movement.AuditEntries.Last().Status == "Updated")
    //    {
    //        movement.AuditEntries.Last().Diff.Any(x => x.Path == "")
    //    }
    //}

    private async Task<LinkResult> FindMovementLinks(Movement movement)
    {
        var chedIds = movement.Items
            .SelectMany(x => x.Documents ?? [])
            .Select(d => MatchIdentifier.FromCds(d.DocumentReference).Identifier)
            .Where(dr => dr != null)
            .Distinct();

        var notifications = await dbContext.Notifications.Where(x => chedIds.Contains(x._MatchReference)).ToListAsync();

        return new LinkResult()
        {
            Movements = [movement],
            Notifications = notifications,
            State = notifications.Any() ? LinkState.Linked : LinkState.NotLinked
        };
    }

    private async Task<LinkResult> FindImportNotificationLinks(ImportNotification importNotification)
    {
        var identifier = MatchIdentifier.FromNotification(importNotification.Id).Identifier;

        var movements = await dbContext.Movements.Where(x => x._MatchReferences.Contains(identifier)).ToListAsync();

        return new LinkResult()
        {
            Movements = movements,
            Notifications = [importNotification],
            State = movements.Any() ? LinkState.Linked : LinkState.NotLinked
        };
    }
}