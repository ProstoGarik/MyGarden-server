using GardenAPI.Data;
using GardenAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GardenAPI.Controllers
{
    [Route("api/plants")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly PlantDbContext _dbContext;
        private readonly ILogger<PlantController> _logger;

        public PlantController(PlantDbContext plantDbContext, ILogger<PlantController> logger)
        {
            _dbContext = plantDbContext;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Plant>> GetPlants()
        {
            return _dbContext.Plants;
        }

        [HttpGet("{plantId:int}")]
        [Authorize]
        public async Task<ActionResult<Plant>> GetPlantById(int plantId)
        {
            return Ok(await _dbContext.Plants.FindAsync(plantId));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Create(Plant plant)
        {
            var roles = User.Claims.ToList();
            if (roles.Any())
            {
                _logger.LogInformation($"Пользователь с ролями: {string.Join(", ", roles)} обратился к контроллеру YourController.");
            }
            else
            {
                _logger.LogInformation("Пользователь не имеет ролей.");
            }

            await _dbContext.Plants.AddAsync(plant);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Update(Plant plant)
        {
            _dbContext.Plants.Update(plant);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{plantId:int}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int plantId)
        {
            var plant = await _dbContext.Plants.FindAsync(plantId);
            _dbContext.Plants.Remove(plant!);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
