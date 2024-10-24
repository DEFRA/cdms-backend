using MongoDB.Driver;

namespace Cdms.Backend.Data;

public class MongoDbContext(IMongoDatabase database)
{
    internal IMongoDatabase Database { get; private set; } = database;
    internal MongoDbTransaction ActiveTransaction { get; private set; }


    //public MongoCollectionSet<Notification> Notifications =>
    //    new(database.GetCollection<Notification>(nameof(Notification)));

    //public MongoCollectionSet<Movement> Movements => new(database.GetCollection<Movement>(nameof(Movement)));

    public async Task<MongoDbTransaction> StartTransaction(CancellationToken cancellationToken = default)
    {
        var session = await Database.Client.StartSessionAsync(cancellationToken: cancellationToken);
        ActiveTransaction = new MongoDbTransaction(session);
        return ActiveTransaction;
    }
}