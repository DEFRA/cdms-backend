

using Cdms.Backend.Data;
using Cdms.Business.Pipelines.Matching;
using Cdms.Model;
using Cdms.Model.Alvs;
using Cdms.Model.Auditing;
using Cdms.Model.Ipaffs;

namespace Cdms.Business.Services;

public class LinkingService : ILinkingService
{
    private readonly string auditId = "TEMP//HACK";
    
    private const string NotificationMatchReference = "_MatchReference";
    private const string MovementMatchReference = "_MatchReferences";

    private readonly IMongoDbContext dbContext;
    
    public LinkingService(IMongoDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<MatchContext> Link(AlvsClearanceRequest clearanceRequest)
    {
        var movement = BuildMovement(clearanceRequest);
        var existingMovement = await dbContext.Movements.Find(movement.Id);

        if (existingMovement != null)
        {
            if (IsMovementSuitableForMatching(existingMovement, movement))
            {
                // Newer version and fields of interest changed
                movement.AuditEntries = existingMovement.AuditEntries;

                var auditEntry = AuditEntry.CreateUpdated(existingMovement.ClearanceRequests[0],
                    movement.ClearanceRequests[0],
                    BuildNormalizedAlvsPath(auditId),
                    movement.ClearanceRequests[0].Header.EntryVersionNumber.GetValueOrDefault(),
                    movement.LastUpdated);
                movement.Update(auditEntry);

                await dbContext.Movements.Update(movement, existingMovement._Etag);
            }
            else
            {
                return new MatchContext()
                {
                    ContinueMatching = false
                };
            }
        }
        else
        {
            var auditEntry = AuditEntry.CreateCreatedEntry(
                movement.ClearanceRequests[0],
                BuildNormalizedAlvsPath(auditId), 
                movement.ClearanceRequests[0].Header.EntryVersionNumber.GetValueOrDefault(),
                movement.LastUpdated);
            movement.Update(auditEntry);
            await dbContext.Movements.Insert(movement);
        }
        
        var chedIds = movement.Items
            .SelectMany(x => x.Documents ?? [])
            .Select(d => GetMatchingRef(d.DocumentReference))
            .Where(dr => dr != null)
            .Distinct();
        
        var notifications = await dbContext.Notifications.FindBy(NotificationMatchReference, chedIds!);
        
        if (notifications.Any())
        {
            return new MatchContext()
            {
                Movements = new List<Movement>() { existingMovement },
                Notifications = notifications.ToList(),
                ContinueMatching = true
            };
        }

        return new MatchContext()
        {
            ContinueMatching = false
        };
    }

    public async Task<MatchContext> Link(ImportNotification notification)
    {
        var existingNotification = await dbContext.Notifications.Find(notification.ReferenceNumber);

        if (existingNotification != null)
        {
            if (IsNotificationSuitableForMatching(existingNotification, notification))
            {
                // Newer version and fields of interest changed
                notification.AuditEntries = existingNotification.AuditEntries;
                notification.Updated(BuildNormalizedIpaffsPath(auditId!), existingNotification);
                await dbContext.Notifications.Update(notification, existingNotification._Etag);
            }
            else
            {
                return new MatchContext()
                {
                    ContinueMatching = false
                };
            }
        }
        else
        {
            notification.Created(BuildNormalizedIpaffsPath(auditId));
            await dbContext.Notifications.Insert(notification);
        }
        
        var matchingRef = GetMatchingRef(notification.ReferenceNumber!);

        var movements = await dbContext.Movements.FindAnyBy(MovementMatchReference, [matchingRef!]);

        if (movements.Any())
        {
            return new MatchContext()
            {
                Movements = movements.ToList(),
                Notifications = new List<ImportNotification>() { notification },
                ContinueMatching = true
            };
        }

        return new MatchContext()
        {
            ContinueMatching = false
        };
    }
    
    private bool IsMovementSuitableForMatching(Movement existing, Movement received)
    {
        if (existing.ClearanceRequests[0].Header.EntryVersionNumber >
            received.ClearanceRequests[0].Header.EntryVersionNumber)
        {
            // Received out of date movement information
            return false;
        }
        
        var existingDocs = existing.Items
            .SelectMany(x => x.Documents ?? [])
            .Select(d => new
            {
                d.DocumentReference
            });
        var receivedDocs = received.Items
            .SelectMany(x => x.Documents ?? [])
            .Select(d => new
            {
                d.DocumentReference
            });
        
        if (existingDocs.Count() != receivedDocs.Count() ||
            !existingDocs.All(receivedDocs.Contains))
        {
            // Delta in received Docs
            return true;
        }
        
        // No deltas in fields we care about
        return false;
    }

    private bool IsNotificationSuitableForMatching(ImportNotification existing, ImportNotification received)
    {
        if (existing.LastUpdated > received.LastUpdated)
        {
            // Received out of date ipaffs information
            return false;
        }
        
        var existingCommodities = existing?.Commodities?
            .Select(c => new
            {
                c.CommodityId,
                c.CommodityDescription
            });
        var receivedCommodities = received?.Commodities?
            .Select(c => new
            {
                c.CommodityId,
                c.CommodityDescription
            });
        
        if (existingCommodities?.Count() != receivedCommodities?.Count() ||
            !existingCommodities.All(receivedCommodities.Contains))
        {
            // Delta in received Commodities
            return true;
        }

        // No deltas in fields we care about
        return false;
    }
    
    // Copy-pasted from consumers, refactor these out to encapsulate audit logging in IAuditable
    private static string BuildNormalizedAlvsPath(string fullPath)
    {
        return fullPath.Replace("RAW/ALVS/", "");
    }

    private static string BuildNormalizedIpaffsPath(string fullPath)
    {
        return fullPath.Replace("RAW/IPAFFS/", "");
    }

    private static Movement BuildMovement(AlvsClearanceRequest request)
    {
        return new Movement()
        {
            Id = request.Header!.EntryReference,
            LastUpdated = request.ServiceHeader?.ServiceCalled,
            EntryReference = request.Header.EntryReference,
            MasteUcr = request.Header.MasterUcr,
            DeclarationType = request.Header.DeclarationType,
            SubmitterTurn = request.Header.SubmitterTurn,
            DeclarantId = request.Header.DeclarantId,
            DeclarantName = request.Header.DeclarantName,
            DispatchCountryCode = request.Header.DispatchCountryCode,
            GoodsLocationCode = request.Header.GoodsLocationCode,
            ClearanceRequests = new List<AlvsClearanceRequest>() { request },
            Items = request.Items?.Select(x => { return x; }).ToList(),
        };
    }
    
    private string? GetMatchingRef(string? documentReference)
    {
        if (documentReference == null) return null;

        var strLength = documentReference.Length;
        if (strLength < 7) return null;
        
        var subStr = documentReference.Substring(strLength - 7, 7);
        
        if (int.TryParse(subStr, out _))
        {
            return subStr;
        }

        return null;
    }
}