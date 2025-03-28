using BombaDeAgua_Api.Models;
using MongoDB.Driver;

namespace BombaDeAgua_Api.Respositories
{
    public interface IHistorialCollection
    {
        Task<List<HistorialModel>> GetAllHistorial();
        Task<HistorialModel> GetHistorial(int id);
        Task AddHistorial(HistorialModel historial);
        Task UpdateHistorial(HistorialModel historial);
        Task RemoveHistorial(int id);
    }
}
