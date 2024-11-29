using Cdms.Backend.Data;
using Cdms.Backend.Data.Extensions;
using Cdms.Metrics;
using Cdms.Model;
using Cdms.Model.Ipaffs;
using Cdms.Model.Relationships;

namespace Cdms.Business.Services;

public class LinkingService(IMongoDbContext dbContext, LinkingMetrics metrics) : ILinkingService
{
    public async Task<LinkResult> Link(LinkContext linkContext, CancellationToken cancellationToken = default)
    {
        var startedAt = TimeProvider.System.GetTimestamp();
        LinkResult result;

        try
        {
            switch (linkContext)
            {
                case MovementLinkContext movementLinkContext:
                    if (!ShouldLink(movementLinkContext))
                    {
                        return new LinkResult(LinkState.NotLinked);
                    }

                    result = await FindMovementLinks(movementLinkContext.ReceivedMovement, cancellationToken);
                    break;
                case ImportNotificationLinkContext notificationLinkContext:
                    if (!ShouldLink(notificationLinkContext))
                    {
                        return new LinkResult(LinkState.NotLinked);
                    }

                    result = await FindImportNotificationLinks(notificationLinkContext.ReceivedImportNotification,
                        cancellationToken);
                    break;
                default: throw new ArgumentException("context type not supported");
            }

            using var transaction = await dbContext.StartTransaction(cancellationToken);
            foreach (var notification in result.Notifications)
            {
                foreach (var movement in result.Movements)
                {
                    notification.AddRelationship(new TdmRelationshipObject()
                    {
                        Links = RelationshipLinks.CreateForNotification(notification),
                        Data =
                        [
                            RelationshipDataItem.CreateFromMovement(notification, movement,
                                notification._MatchReference)
                        ]
                    });

                    movement.AddRelationship(new TdmRelationshipObject()
                    {
                        Links = RelationshipLinks.CreateForMovement(movement),
                        Data =
                        [
                            RelationshipDataItem.CreateFromNotification(notification, movement,
                                notification._MatchReference)
                        ]
                    });

                    await dbContext.Movements.Update(movement, movement._Etag, transaction, cancellationToken);
                    await dbContext.Notifications.Update(notification, notification._Etag, transaction,
                        cancellationToken);
                }
            }

            await transaction.CommitTransaction(cancellationToken);
        }
        catch (Exception e)
        {
            metrics.Faulted(e);
            throw;
        }
        finally
        {
            var e = TimeProvider.System.GetElapsedTime(startedAt);
            metrics.Completed(e.TotalMilliseconds);
        }

        if (result.State == LinkState.Linked)
        {
            metrics.Linked<Movement>(result.Movements.Count);
            metrics.Linked<ImportNotification>(result.Notifications.Count);
        }

        return result;
    }

    private static bool ShouldLink(MovementLinkContext movContext)
    {
        if (movContext.ExistingMovement is null) return true;

        var existingItems = movContext.ExistingMovement.Items is null ? [] : movContext.ExistingMovement.Items;
        var receivedItems = movContext.ReceivedMovement.Items is null ? [] : movContext.ReceivedMovement.Items;

        // Diff movements for fields of interest
        var existingDocs = existingItems
            .SelectMany(x => x.Documents ?? [])
            .Select(d => new
            {
                d.DocumentReference
            }).ToList();

        var receivedDocs = receivedItems
            .SelectMany(x => x.Documents ?? [])
            .Select(d => new
            {
                d.DocumentReference
            }).ToList();

        if (existingDocs.Count != receivedDocs.Count ||
            !existingDocs.TrueForAll(receivedDocs.Contains))
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
            existingCommodities?.TrueForAll(receivedCommodities!.Contains) != true)
        {
            // Delta in received Commodities
            return true;
        }

        return false;
    }

    private async Task<LinkResult> FindMovementLinks(Movement movement, CancellationToken cancellationToken)
    {
        var notifications = await dbContext.Notifications.Where(x => movement._MatchReferences.Contains(x._MatchReference)).ToListAsync(cancellationToken: cancellationToken);

        return new LinkResult(notifications.Any() ? LinkState.Linked : LinkState.NotLinked)
        {
            Movements = [movement],
            Notifications = notifications
        };
    }

    private async Task<LinkResult> FindImportNotificationLinks(ImportNotification importNotification, CancellationToken cancellationToken)
    {
        var movements = await dbContext.Movements.Where(x => x._MatchReferences.Contains(importNotification._MatchReference)).ToListAsync(cancellationToken);

        return new LinkResult(movements.Any() ? LinkState.Linked : LinkState.NotLinked)
        {
            Movements = movements,
            Notifications = [importNotification]
        };
    }
}