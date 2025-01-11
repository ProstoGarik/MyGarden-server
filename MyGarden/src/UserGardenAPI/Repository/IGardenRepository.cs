using MongoDB.Driver;
using UserGardenAPI.Model;

public interface IGardenRepository
{
    Task<List<Garden>> GetGardensByUserId(string userId);
    Task<Garden> GetGardenById(string gardenId);
    Task CreateGarden(Garden garden);
    Task<ReplaceOneResult> UpdateGarden(string gardenId, Garden garden);
    Task<DeleteResult> DeleteGarden(string gardenId);
}