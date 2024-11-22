using Cdms.Backend.Data;
using Cdms.Backend.Data.Extensions;
using Cdms.Model;
using Cdms.Model.Ipaffs;
using Cdms.Model.Relationships;
using Xunit.Sdk;

namespace Cdms.Business.Services;

public class LinkingService(IMongoDbContext dbContext) : ILinkingService
{
    public async Task<LinkResult> Link(LinkContext linkContext)
    {
        LinkResult result;
        switch (linkContext)
        {
            case MovementLinkContext movementLinkContext:
                if (!ShouldLink(movementLinkContext))
                {
                    return new LinkResult(LinkState.NotLinked);
                }
                
                result = await FindMovementLinks(movementLinkContext.ReceivedMovement);
                break;
            case ImportNotificationLinkContext notificationLinkContext:
                if (!ShouldLink(notificationLinkContext))
                {
                    return new LinkResult(LinkState.NotLinked);
                }
                
                result = await FindImportNotificationLinks(notificationLinkContext.ReceivedImportNotification);
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

    private static bool ShouldLink(MovementLinkContext movContext)
    {
        if (movContext.ExistingMovement is null) return true;
        
        // Diff movements for fields of interest
        var existingDocs = movContext.ExistingMovement.Items
            .SelectMany(x => x.Documents ?? [])
            .Select(d => new
            {
                d.DocumentReference
            }).ToList();
        
        var receivedDocs = movContext.ReceivedMovement.Items
            .SelectMany(x => x.Documents ?? [])
            .Select(d => new
            {
                d.DocumentReference
            }).ToList();
        
        if (existingDocs.Count != receivedDocs.Count ||
            !existingDocs.All(receivedDocs.Contains))
        {
            // Delta in received Docs
            return true;
        }
        
        return false;
    }
    
    private static bool ShouldLink(ImportNotificationLinkContext notifContext)
    {
        if (notifContext.ExistingImportNotification is null) return true;
        
        var existingCommodities = notifContext.ExistingImportNotification.Commodities?
            .Select(c => new
            {
                c.CommodityId,
                c.CommodityDescription
            }).ToList();
        var receivedCommodities = notifContext.ReceivedImportNotification.Commodities?
            .Select(c => new
            {
                c.CommodityId,
                c.CommodityDescription
            }).ToList();
        
        if (existingCommodities?.Count != receivedCommodities?.Count ||
            existingCommodities?.All(receivedCommodities!.Contains) != true)
        {
            // Delta in received Commodities
            return true;
        }
        
        return false;
    }

    private async Task<LinkResult> FindMovementLinks(Movement movement)
    {
        var chedIds = movement.Items
            .SelectMany(x => x.Documents ?? [])
            .Where(d => d?.DocumentReference != null)
            .Select(d => MatchIdentifier.FromCds(d!.DocumentReference!).Identifier)
            .Distinct();

        var notifications = await dbContext.Notifications.Where(x => chedIds.Contains(x._MatchReference)).ToListAsync();

        return new LinkResult(notifications.Any() ? LinkState.Linked : LinkState.NotLinked)
        {
            Movements = [movement],
            Notifications = notifications
        };
    }

    private async Task<LinkResult> FindImportNotificationLinks(ImportNotification importNotification)
    {
        var identifier = MatchIdentifier.FromNotification(importNotification!.Id!).Identifier;

        var movements = await dbContext.Movements.Where(x => x._MatchReferences.Contains(identifier)).ToListAsync();

        return new LinkResult(movements.Any() ? LinkState.Linked : LinkState.NotLinked)
        {
            Movements = movements,
            Notifications = [importNotification]
        };
    }
}