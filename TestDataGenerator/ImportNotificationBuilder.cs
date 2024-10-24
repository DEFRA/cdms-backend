using System.Runtime.Serialization;
using Cdms.Types.Ipaffs;

namespace TestDataGenerator;

public class ImportNotificationBuilder : ImportNotificationBuilder<ImportNotification>
{
    public ImportNotificationBuilder()
    {
    }

    public ImportNotificationBuilder(string file) : base(file)
    {
    }
}

public class ImportNotificationBuilder<T> : BuilderBase<T, ImportNotificationBuilder<T>>
    where T : ImportNotification, new()
{
    public ImportNotificationBuilder() : base()
    {
    }
    
    public ImportNotificationBuilder(string file) : base(file)
    {
    }

    public static ImportNotificationBuilder<T> FromFile(string file)
    {
        return new ImportNotificationBuilder<T>(file);
    }
    
    public ImportNotificationBuilder<T> WithReferenceNumber(IpaffsImportNotificationTypeEnum chedType, int item)
    {
        var prefix = chedType switch
        {
            IpaffsImportNotificationTypeEnum.Cveda => "CHEDA",
            IpaffsImportNotificationTypeEnum.Cvedp => "CHEDP",
            IpaffsImportNotificationTypeEnum.Chedpp => "CHEDPP",
            IpaffsImportNotificationTypeEnum.Ced => "CHEDD",
            _ => throw new ArgumentOutOfRangeException(nameof(chedType), chedType, null),
        };

        // TODO : We may need a way to guarantee these don't collide?....
        return With(x => x.ReferenceNumber, $"{prefix}.GB.{DateTime.Now.Year}.{CreateRandomInt(7)}");
    }

    // public ImportNotificationBuilder<T> WithStatus(Status status)
    // {
    //     var field = typeof(Status).GetField(status.ToString());
    //     var description = field
    //         ?.GetCustomAttributes(typeof(EnumMemberAttribute), false)
    //         .Cast<EnumMemberAttribute>()
    //         .Select(x => x.Value)
    //         .FirstOrDefault();
    //
    //     return With(x => x.Status, description ?? status.ToString("G"));
    // }
}