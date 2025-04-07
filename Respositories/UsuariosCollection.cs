using BombaDeAgua_Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BombaDeAgua_Api.Respositories
{
    public class UsuariosCollection : IUsuariosCollection
    {
        internal MongoDbRepository _repository = new MongoDbRepository();
        private IMongoCollection<UsuarioModel> Collection;
        private DatabaseHelper _databaseHelper; // Agregar DatabaseHelper

        public UsuariosCollection()
        {
            Collection = _repository.Database.GetCollection<UsuarioModel>("Usuarios");
            _databaseHelper = new DatabaseHelper(); // Inicializar DatabaseHelper
        }

        public async Task AddUsuario(UsuarioModel usuario)
        {
            usuario.Id = await _databaseHelper.GetNextSequenceValue("Usuarios"); // Obtener nuevo ID
            await Collection.InsertOneAsync(usuario);
        }

        public async Task<List<UsuarioModel>> GetAllUsuarios()
        {
            return await Collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<UsuarioModel> GetUsuario(int id)
        {
            return await Collection.FindAsync(new BsonDocument { { "Id", id } }).Result.FirstOrDefaultAsync();
        }
        public async Task<UsuarioModel> GetUsuario(string email)
        {
            return await Collection.FindAsync(new BsonDocument { { "Correo", email } }).Result.FirstOrDefaultAsync();
        }
        public async Task<UsuarioModel> GetUsuarioById(string contrasena)
        {
            return await Collection.FindAsync(new BsonDocument { { "Contrasena", contrasena } }).Result.FirstOrDefaultAsync();
        }
        public async Task<UsuarioModel> GetUsuariobyemailandcontrasena(string email, string contrasena)
        {
            var filter = Builders<UsuarioModel>.Filter.And(
                Builders<UsuarioModel>.Filter.Eq(u => u.Correo, email),
                Builders<UsuarioModel>.Filter.Eq(u => u.Contrasena, contrasena)
            );
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task RemoveUsuario(int id)
        {
            var filter = Builders<UsuarioModel>.Filter.Eq(s => s.Id, id);
            await Collection.DeleteOneAsync(filter);
        }

        public async Task UpdateUsuario(UsuarioModel usuario)
        {
            Console.WriteLine($"Actualizando usuario con ID: {usuario.Id}");

            var filter = Builders<UsuarioModel>.Filter.Eq(s => s.Id, usuario.Id);
            var result = await Collection.ReplaceOneAsync(filter, usuario);

            if (result.MatchedCount == 0)
            {
                Console.WriteLine("No se encontró un usuario con ese ID.");
            }
            else
            {
                Console.WriteLine("Usuario actualizado correctamente.");
            }
        }
    }
}
