using Microsoft.AspNetCore.Mvc;
using UserGardenAPI.Model;

namespace UserGardenAPI.Controllers
{
    [ApiController]
    [Route("api/gardens")]
    public class GardenController(IGardenRepository repository) : ControllerBase
    {
        private readonly IGardenRepository _gardenRepository = repository;

        [HttpGet("/user/{userId}")]
        public async Task<ActionResult<IEnumerable<Garden>>> GetGardenByUserId(string userId)
        {
            var gardens = await _gardenRepository.GetGardensByUserId(userId);
            if (gardens.Count() < 1) return NotFound();
            return Ok(gardens);
        }

        [HttpGet("{gardenId}")]
        public async Task<ActionResult<Garden>> GetGardenById(string gardenId)
        {
            var garden = await _gardenRepository.GetGardenById(gardenId);
            if (garden == null)
            {
                return NotFound();
            }

            return Ok(garden);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGarden([FromBody] Garden garden)
        {
            try
            {
                await _gardenRepository.CreateGarden(garden);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("{gardenId}")]
        public async Task<IActionResult> UpdateGarden(string gardenId, [FromBody] Garden garden) => 
            (await _gardenRepository.UpdateGarden(gardenId, garden)).ModifiedCount > 0 ? Ok() : NotFound();


        [HttpDelete("{gardenId}")]
        public async Task<IActionResult> DeleteGarden(string gardenId) => 
            (await _gardenRepository.DeleteGarden(gardenId)).DeletedCount > 0 ? Ok() : NotFound();


    }
}
