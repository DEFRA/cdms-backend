//using Cdms.Model.Extensions;
//using JsonApiDotNetCore.Resources.Annotations;

//namespace Cdms.Model.Relationships;

//public sealed class RelationshipLinks
//{
//    [Attr]
//    public string Self { get; set; }

//    [Attr]
//    public string Related { get; set; }

//    public static RelationshipLinks CreateForMovement(Movement movement)
//    {
//        return new RelationshipLinks()
//        {
//            Self = LinksBuilder.Movement.BuildSelfLink(movement.Id),
//            Related = LinksBuilder.Movement.BuildRelatedMovementLink(movement.Id)
//        };
//    }

//    public static RelationshipLinks CreateForNotification(Notification notification)
//    {
//        return new RelationshipLinks()
//        {
//            Self = LinksBuilder.Notification.BuildSelfLink(notification.Id),
//            Related = LinksBuilder.Notification.BuildRelatedMovementLink(notification.Id)
//        };
//    }
//}