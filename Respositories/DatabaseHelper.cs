using MongoDB.Driver;
using BombaDeAgua_Api.Models;
namespace BombaDeAgua_Api.Respositories
{
    public class DatabaseHelper
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Counter> _counter;

        public DatabaseHelper()
        {
            var connectionString = "mongodb+srv://KingTempest:KingTempest012@cluster0.9huvs.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("BombaDeAguaDB");
            _counter = _database.GetCollection<Counter>("Counters");

            var filter = Builders<Counter>.Filter.Eq(c => c.Id, "Usuarios");
            var existingCounter = _counter.Find(filter).FirstOrDefault();

            if (existingCounter == null)
            {
                _counter.InsertOne(new Counter { Id = "Usuarios", SequenceValue = 1 });
            }
        }
        public async Task<int> GetNextSequenceValue(string sequenceName)
        {
            var filter = Builders<Counter>.Filter.Eq(x => x.Id, sequenceName);
            var update = Builders<Counter>.Update.Inc(x => x.SequenceValue, 1);
            var options = new FindOneAndUpdateOptions<Counter>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };
            var result = await _counter.FindOneAndUpdateAsync(filter, update, options);
            return result.SequenceValue;
        }
    }
}
