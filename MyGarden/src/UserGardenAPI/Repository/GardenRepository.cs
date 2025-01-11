using MongoDB.Driver;
using UserGardenAPI.Model;

public class GardenRepository : IGardenRepository
{
    private readonly IMongoCollection<Garden> _gardens;

    public GardenRepository(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("GardenDb");
        _gardens = database.GetCollection<Garden>("Gardens");
    }

    public async Task CreateGarden(Garden garden)
    {
        try
        {
            await _gardens.InsertOneAsync(garden);
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving gardens: " + ex.Message);
        }
    }

    public async Task<DeleteResult> DeleteGarden(string gardenId) => await _gardens.DeleteOneAsync(garden => garden.Id == gardenId);

    public async Task<Garden> GetGardenById(string gardenId) => await _gardens.Find(garden => garden.Id == gardenId).FirstOrDefaultAsync();

    public async Task<List<Garden>> GetGardensByUserId(string userId) => await _gardens.Find(garden => garden.UserId == userId).ToListAsync();

    public async Task<ReplaceOneResult> UpdateGarden(string gardenId, Garden garden) => await _gardens.ReplaceOneAsync(garden => garden.Id == gardenId, garden);
}