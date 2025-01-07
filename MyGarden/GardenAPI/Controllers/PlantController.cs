using GardenAPI.Data;
using GardenAPI.Entities;
using GardenAPI.Service.Plants;
using GardenAPI.Transfer.Plant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardenAPI.Controllers
{
    [Route("api/plant")]
    [ApiController]
    public class PlantController(PlantService dataEntityService) : ControllerBase
    {
        /// <summary>
        ///     Сервис моделей.
        /// </summary>
        private PlantService DataEntityService { get; } = dataEntityService;

        /// <summary>
        ///     Получить список растений.
        ///     Если идентификаторы не указаны, возвращается список со всеми растениями.
        ///     Иначе возвращается список с указанными растениями, либо пустой список.
        /// </summary>
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Результат операции со списком растений.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantDTO>>> Get([FromQuery] List<int> ids)
        {
            var plants = (await DataEntityService.Get(DataEntityService.DataContext.Plants, ids)).Select(x=>x.ToDTO());
            //var plants = DataEntityService.DataContext.Plants.Include(p=>p.Group).Include(p => p.LightNeed).Include(p => p.WateringNeed).Include(p => p.Stage).Select(x=>x.ToDTO());
            return Ok(plants);
        }

        /// <summary>
        ///     Сохранить растения.
        /// </summary>
        /// <param name="entities">Список растений.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<RequestPlantDTO> entities)
        {
            var status = await DataEntityService.Set(DataEntityService.DataContext.Plants, entities.Select(x=>x.ToEntity()).ToList());

            if (!status)
            {
                return BadRequest("No plants were saved!");
            }

            return Ok();
        }

        /// <summary>
        ///     Удалить растения.
        /// </summary>
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] List<int> ids)
        {
            var status = await DataEntityService.Remove(DataEntityService.DataContext.Plants, ids);

            if (!status)
            {
                return BadRequest("No plants were deleted!");
            }

            return Ok();
        }
    }
}
