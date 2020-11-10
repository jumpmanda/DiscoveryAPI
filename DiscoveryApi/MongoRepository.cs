using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DiscoveryApi
{
    public class MongoRepository
    {
        private MongoClient _client;
        public IMongoDatabase db;

        public MongoRepository(string connectionString, string dbName)
        {
            _client = new MongoClient(connectionString) ?? throw new NullReferenceException("Mongo client is null.");
            db = _client.GetDatabase(dbName) ?? throw new NullReferenceException("Database could not be retrieved.");

        }

        #region Insert
        public void Insert<T>(string collection, T item)
        {
            var dbCollection = db.GetCollection<T>(collection);
            dbCollection.InsertOne(item);
        }

        public async Task InsertAsync<T>(string collection, T item)
        {
            var dbCollection = db.GetCollection<T>(collection);
            await dbCollection.InsertOneAsync(item);
        }
        #endregion

        #region Query
        public List<T> FindAll<T>(string collection)
        {
            var dbCollection = db.GetCollection<T>(collection);
            var results = dbCollection.Find(new BsonDocument());
            return results.ToList<T>();
        }

        #endregion

    }
}
