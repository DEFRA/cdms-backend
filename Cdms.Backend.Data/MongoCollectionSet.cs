using System.Collections;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Cdms.Backend.Data
{
    public class MongoCollectionSet<T>(MongoDbContext dbContext) : IMongoQueryable<T> where T : IDataEntity
    {
        private readonly IMongoCollection<T> collection = dbContext.Database.GetCollection<T>(typeof(T).Name);
        private IMongoQueryable<T> EntityQueryable => collection.AsQueryable();

        public IEnumerator<T> GetEnumerator()
        {
            return EntityQueryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return EntityQueryable.GetEnumerator();
        }

        public Type ElementType => EntityQueryable.ElementType;
        public Expression Expression => EntityQueryable.Expression;

        IMongoQueryProvider IMongoQueryable.Provider => EntityQueryable.Provider;

        public QueryableExecutionModel GetExecutionModel()
        {
            return EntityQueryable.GetExecutionModel();
        }

        public BsonDocument[] LoggedStages => EntityQueryable.LoggedStages;
        public IQueryProvider Provider => EntityQueryable.Provider;

        public IAsyncCursor<T> ToCursor(CancellationToken cancellationToken = new CancellationToken())
        {
            return EntityQueryable.ToCursor(cancellationToken);
        }

        public Task<IAsyncCursor<T>> ToCursorAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return EntityQueryable.ToCursorAsync(cancellationToken);
        }

        public Task<T> Find(string id)
        {
            return EntityQueryable.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task Insert(T item, MongoDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            IClientSessionHandle session =
                transaction is null ? dbContext.ActiveTransaction?.Session : transaction.Session;
            return session is not null
                ? collection.InsertOneAsync(session, item, cancellationToken: cancellationToken)
                : collection.InsertOneAsync(item, cancellationToken: cancellationToken);
        }
    }
}