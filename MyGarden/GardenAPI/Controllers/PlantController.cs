using GardenAPI.Data;
using GardenAPI.Entities;
using GardenAPI.Service.Plants;
using GardenAPI.Transfer.Plant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        ///     Получить список курсов.
        ///     Если идентификаторы не указаны, возвращается список со всеми курсами.
        ///     Иначе возвращается список с указанными курсами, либо пустой список.
        /// </summary>
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Результат операции со списком курсов.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantDTO>>> Get([FromQuery] List<int> ids)
        {
            return Ok((await DataEntityService.Get(DataEntityService.DataContext.Plants, ids)).Select(x=>x.ToDTO()));
        }

        /// <summary>
        ///     Сохранить курсы.
        /// </summary>
        /// <param name="entities">Список курсов.</param>
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
        ///     Удалить курсы.
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
