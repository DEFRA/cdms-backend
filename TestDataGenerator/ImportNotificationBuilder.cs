using Cdms.Types.Ipaffs;
using Cdms.Common.Extensions;
using TestDataGenerator.Helpers;

namespace TestDataGenerator;

public class ImportNotificationBuilder : ImportNotificationBuilder<ImportNotification>
{
    private ImportNotificationBuilder()
    {
    }

    private ImportNotificationBuilder(string file) : base(file)
    {
    }
    
    public static ImportNotificationBuilder<ImportNotification> FromFile(string file)
    {
        return new ImportNotificationBuilder(file)
            .WithClean();
    }
    
    public static ImportNotificationBuilder<ImportNotification> Default()
    {
        return new ImportNotificationBuilder()
            .WithClean();
    }
}

public class ImportNotificationBuilder<T> : BuilderBase<T, ImportNotificationBuilder<T>>
    where T : ImportNotification, new()
{
    protected ImportNotificationBuilder()
    {
    }

    protected ImportNotificationBuilder(string file) : base(file)
    {
    }
    
    /// <summary>
    /// Allows any customisations needed, such as removing problems with serialisation, e.g. Do(n => Array.ForEach(n.PartOne!.Commodities!.ComplementParameterSets!, x => x.KeyDataPairs = null));
    /// </summary>
    protected ImportNotificationBuilder<T> WithClean()
    {
        // TODO : 
        
        return this; 
    }

    public ImportNotificationBuilder<T> WithRandomCommodities(int min, int max)
    {
        var commodityCount = CreateRandomInt(min, max);

        return Do(n =>
        {
            var commodities = Enumerable.Range(0, commodityCount)
                .Select(_ => n.PartOne!.Commodities!.CommodityComplements![0]
                ).ToArray();
            
            n.PartOne!.Commodities!.CommodityComplements = commodities;
        });
    }

    public ImportNotificationBuilder<T> WithReferenceNumber(ImportNotificationTypeEnum chedType)
    {
        var prefix = chedType.ConvertToChedType();

        // TODO : We may need a way to guarantee these don't collide?....
        return Do(x => x.ReferenceNumber = $"{prefix}.GB.{DateTime.Now.Year}.{CreateRandomInt(7)}");
    }

    public ImportNotificationBuilder<T> WithEntryDate(DateTime entryDate)
    {
        return Do(x => x.LastUpdated = entryDate);
    }

    public ImportNotificationBuilder<T> WithCommodity(string commodityCode, string description, int netWeight)
    {
        return Do(n => n.PartOne!.Commodities!.TotalNetWeight = netWeight)
            .Do(n => n.PartOne!.Commodities!.TotalGrossWeight = netWeight)
            .Do(n => n.PartOne!.Commodities!.CommodityComplements![0].SpeciesId = commodityCode)
            .Do(n => n.PartOne!.Commodities!.CommodityComplements![0].SpeciesClass = commodityCode)
            .Do(n => n.PartOne!.Commodities!.CommodityComplements![0].CommodityDescription = description)
            .Do(n => n.PartOne!.Commodities!.CommodityComplements![0].ComplementName = description)
            .Do(n => n.PartOne!.Commodities!.CommodityComplements![0].SpeciesName = description)
            .Do(n => n.PartOne!.Commodities!.CommodityComplements![0].SpeciesNomination = description)
            .Do(n => n.PartOne!.Commodities!.ComplementParameterSets![0].SpeciesId = commodityCode);
    }

    protected override ImportNotificationBuilder<T> Validate()
    {
        return Do(n =>
        {
            n.ReferenceNumber.AssertHasValue();
        });
    }
}