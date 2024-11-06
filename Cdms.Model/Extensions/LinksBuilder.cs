namespace Cdms.Model.Extensions;

public static class LinksBuilder
{
    public static class Notification
    {
        public static string BuildSelfNotificationLink(string id)
        {
            return LinksBuilder.BuildSelfLink("import-notifications", id);
        }

        public static string BuildRelatedMovementLink(string id)
        {
            return LinksBuilder.BuildRelatedLink("import-notifications", id, "movements");
        }
    }

    public static class Movement
    {
        public static string BuildSelfMovementLink(string id)
        {
            return LinksBuilder.BuildSelfLink("movements", id);
        }

        public static string BuildRelatedMovementLink(string id)
        {
            return LinksBuilder.BuildRelatedLink("movements", id, "import-notifications");
        }
    }

    public static class Gmr
    {
        public static string BuildSelfRelationshipCustomsLink(string id)
        {
            return LinksBuilder.BuildSelfRelationshipLink("gmr", id, "import-notifications");
        }

        public static string BuildSelfRelationshipTransitsLink(string id)
        {
            return LinksBuilder.BuildSelfRelationshipLink("gmr", id, "movement");
        }

        public static string BuildRelatedTransitLink(string id)
        {
            return LinksBuilder.BuildSelfLink("movements", id);
        }

        public static string BuildRelatedCustomsLink(string id)
        {
            return LinksBuilder.BuildSelfLink("import-notifications", id);
        }
    }

    public static string BuildSelfLink(string type, string id)
    {
        return $"/api/{type}/{id}";
    }

    public static string BuildRelatedLink(string type, string id, string related)
    {
        return $"/api/{type}/{id}/{related}";
    }

    public static string BuildSelfRelationshipLink(string type, string id, string relationship)
    {
        return $"/api/{type}/{id}/relationships/{relationship}";
    }
}