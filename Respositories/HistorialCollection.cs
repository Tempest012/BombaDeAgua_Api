using BombaDeAgua_Api.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace BombaDeAgua_Api.Respositories
{
    public class HistorialCollection : IHistorialCollection
    {
        internal MongoDbRepository _repository = new MongoDbRepository();
        private IMongoCollection<HistorialModel> Collection;
        private DatabaseHelper _databaseHelper; // Agregar DatabaseHelper

        public HistorialCollection()
        {
            Collection = _repository.Database.GetCollection<HistorialModel>("Historial");
            _databaseHelper = new DatabaseHelper();
        }
        public async Task AddHistorial(HistorialModel historial)
        {
            historial.Id = await _databaseHelper.GetNextSequenceValue("Historial");
            historial.Fecha = DateTime.UtcNow; // Asigna la fecha actual
            await Collection.InsertOneAsync(historial);
        }


        public Task<List<HistorialModel>> GetAllHistorial()
        {
            return Collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<HistorialModel> GetHistorial(int id)
        {
            return await Collection.FindAsync(new BsonDocument { { "Id", id } }).Result.FirstOrDefaultAsync();
        }

        public async Task RemoveHistorial(int id)
        {
            var filter = Builders<HistorialModel>.Filter.Eq(s => s.Id, id);
            await Collection.DeleteOneAsync(filter);
        }

        public async Task UpdateHistorial(HistorialModel historial)
        {
            var filter = Builders<HistorialModel>.Filter.Eq(s => s.Id, historial.Id);
            var result = await Collection.ReplaceOneAsync(filter, historial);
        }
    }
}
