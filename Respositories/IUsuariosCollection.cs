
using BombaDeAgua_Api.Models;
using MongoDB.Driver;

namespace BombaDeAgua_Api.Respositories
{
    public interface IUsuariosCollection
    {
        Task<List<UsuarioModel>> GetAllUsuarios();
        Task<UsuarioModel> GetUsuario(int id);
        Task AddUsuario(UsuarioModel usuario);
        Task UpdateUsuario(UsuarioModel usuario);
        Task RemoveUsuario(int id);
        
    }
}
