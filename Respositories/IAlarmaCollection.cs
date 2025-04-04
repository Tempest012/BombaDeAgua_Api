using BombaDeAgua_Api.Models;
using MongoDB.Driver;
namespace BombaDeAgua_Api.Respositories
{
    public interface IAlarmaCollection
    {
        Task<List<AlarmaModel>> GetAllAlarmas();
        Task<AlarmaModel> GetAlarma(int id);
        Task AddAlarma(AlarmaModel alarma);
        Task UpdateAlarma(AlarmaModel alarma);
        Task RemoveAlarma(int id);
    }
}
