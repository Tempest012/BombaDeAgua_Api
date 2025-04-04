using BombaDeAgua_Api.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace BombaDeAgua_Api.Respositories
{
    public class AlarmaCollection : IAlarmaCollection
    {
        internal MongoDbRepository _repository = new MongoDbRepository();
        private IMongoCollection<AlarmaModel> Collection;
        private DatabaseHelper _databaseHelper; // Agregar DatabaseHelper
        public AlarmaCollection()
        {
            Collection = _repository.Database.GetCollection<AlarmaModel>("Alarmas");
            _databaseHelper = new DatabaseHelper();
        }
        public async Task AddAlarma(AlarmaModel alarma)
        {
            alarma.Id = await _databaseHelper.GetNextSequenceValue("Alarmas");
            await Collection.InsertOneAsync(alarma);
        }
        public Task<List<AlarmaModel>> GetAllAlarmas()
        {
            return Collection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task<AlarmaModel> GetAlarma(int id)
        {
            return await Collection.FindAsync(new BsonDocument { { "Id", id } }).Result.FirstOrDefaultAsync();
        }

        public async Task RemoveAlarma(int id)
        {
            var filter = Builders<AlarmaModel>.Filter.Eq(s => s.Id, id);
            await Collection.DeleteOneAsync(filter);
        }

        public async Task UpdateAlarma(AlarmaModel alarma)
        {
            var filter = Builders<AlarmaModel>.Filter.Eq(s => s.Id, alarma.Id);
            var result = await Collection.ReplaceOneAsync(filter, alarma);
        }

    }
}
