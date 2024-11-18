using System.Collections;
using System.Linq.Expressions;
using Cdms.Model.Data;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Cdms.Backend.Data.Mongo
{
    public class MongoCollectionSet<T>(MongoDbContext dbContext, string collectionName = null)
        : IMongoCollectionSet<T> where T : IDataEntity
    {
        private readonly IMongoCollection<T> collection = string.IsNullOrEmpty(collectionName)
            ? dbContext.Database.GetCollection<T>(typeof(T).Name)
            : dbContext.Database.GetCollection<T>(collectionName);

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
        public IQueryProvider Provider => EntityQueryable.Provider;

        public Task<T> Find(string id)
        {
            return EntityQueryable.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<T>> FindBy<TField>(Expression<Func<T, TField>> expression, TField value)
        {
            var filter = Builders<T>.Filter.Eq(new ExpressionFieldDefinition<T, TField>(expression), value);

            return collection.Find(filter).ToListAsync();
        }

        public Task<List<T>> FindInBy<TField>(Expression<Func<T, TField>> expression, IEnumerable<TField> values)
        {
            var filter = Builders<T>.Filter.In(new ExpressionFieldDefinition<T, TField>(expression), values);

            return collection.Find(filter).ToListAsync();
        }


        public Task<List<T>> FindAnyBy(string fieldName, IEnumerable<int> values)
        {
            var filter = Builders<T>.Filter.AnyIn(new StringFieldDefinition<T, int>(fieldName), values);
            return collection.Find(filter).ToListAsync();
        }

        public Task Insert(T item, IMongoDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            item._Etag = BsonObjectIdGenerator.Instance.GenerateId(null, null).ToString();
            var session =
                transaction is null ? dbContext.ActiveTransaction?.Session : transaction.Session;
            return session is not null
                ? collection.InsertOneAsync(session, item, cancellationToken: cancellationToken)
                : collection.InsertOneAsync(item, cancellationToken: cancellationToken);
        }

        public async Task Update(T item, string etag, IMongoDbTransaction transaction = null,
            CancellationToken cancellationToken = default)
        {
            var builder = Builders<T>.Filter;

            var filter = builder.Eq(x => x.Id, item.Id) & builder.Eq(x => x._Etag, etag);

            item._Etag = BsonObjectIdGenerator.Instance.GenerateId(null, null).ToString();

            var session =
                transaction is null ? dbContext.ActiveTransaction?.Session : transaction.Session;
            var updateResult = session is not null
                ? await collection.ReplaceOneAsync(session, filter, item,
                    cancellationToken: cancellationToken)
                : await collection.ReplaceOneAsync(filter, item,
                    cancellationToken: cancellationToken);

            if (updateResult.ModifiedCount == 0)
            {
                throw new ConcurrencyException("Concurrency Error, change this to a Concurrency exception");
            }
        }
    }
}