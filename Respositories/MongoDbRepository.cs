using MongoDB.Driver;

namespace BombaDeAgua_Api.Respositories
{
    public class MongoDbRepository
    {
        public MongoClient Client;

        public IMongoDatabase Database;

        public MongoDbRepository()
        {
            var connectionString = "mongodb+srv://KingTempest:KingTempest012@cluster0.9huvs.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase("BombaDeAguaDB");
        }
    }
}
