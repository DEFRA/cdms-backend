//using Cdms.Model.Extensions;
//using JsonApiDotNetCore.Resources.Annotations;
//using Notification = Tdm.Model.Ipaffs.Notification;

//namespace Cdms.Model.Relationships;

//public sealed class RelationshipDataItem 
//{
//    [Attr]
//    public bool Matched { get; set; } = default!;

//    [Attr]
//    public string Type { get; set; }

//    [Attr]
//    public string Id { get; set; }

//    [Attr]
//    public ResourceLink Links { get; set; }
    
//    [Attr]
//    public int? SourceItem { get; set; } = default!;

//    [Attr]
//    public int? DestinationItem { get; set; } = default!;

//    //[Attr]
//    //public Dictionary<string, string> AdditionalInformation { get; set; 

//    public int? MatchingLevel { get; set; }

//    public static RelationshipDataItem CreateFromNotification(Notification notification, Movement movement, string matchReference, bool matched = true, string reason = null)
//    {
//        Dictionary<string, string> additionalInfo = new Dictionary<string, string>() { { "matchingLevel", "1" } };

//        if (!string.IsNullOrEmpty(reason))
//        {
//            additionalInfo.Add("reason", reason);
//        }

//        return new RelationshipDataItem()
//        {
//            Matched = matched,
//            Type = "notifications",
//            Id = notification.Id,
//            SourceItem = movement.Items
//                .FirstOrDefault(x => x.Documents.Any(d => d.DocumentReference.Contains(matchReference)))
//                ?.ItemNumber,
//            DestinationItem = notification.Commodities?.FirstOrDefault()?.ComplementId,
//            Links = new ResourceLink() { Self = LinksBuilder.Notification.BuildSelfLink(notification.Id) },
//            MatchingLevel = 1
//        };
//    }

//    public static RelationshipDataItem CreateFromMovement(Notification notification, Movement movement, string matchReference, bool matched = true, string reason = null)
//    {
//        Dictionary<string, string> additionalInfo = new Dictionary<string, string>() { { "matchingLevel", "1" } };

//        if (!string.IsNullOrEmpty(reason))
//        {
//            additionalInfo.Add("reason", reason);
//        }
//        return new RelationshipDataItem()
//        {
//            Matched = matched,
//            Type = "movements",
//            Id = movement.Id,
//            SourceItem = notification?.Commodities?.FirstOrDefault()?.ComplementId,
//            DestinationItem = movement.Items
//                .FirstOrDefault(x => x.Documents.Any(d => d.DocumentReference.Contains(matchReference)))
//                ?.ItemNumber,
//            Links = new ResourceLink()
//            {
//                Self = LinksBuilder.Movement.BuildSelfLink(movement.Id)
//            },
//            MatchingLevel = 1
//        };
//    }

//}