using Microsoft.AspNetCore.Mvc;
using MyGarden.Server.Entities;
using MyGarden.Server.Entity.Events;
using MyGarden.Server.Service;
using MyGarden.Server.Service.Common;
using MyGarden.Server.Service.Plants;

namespace MyGarden.Server.Controllers
{
    /// <summary>
    ///     Контроллер для работы с объектами типа <see cref="Plant" />.
    /// </summary>
    /// <param name="dataEntityService">Сервис моделей.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class EventController(EventService dataEntityService) : ControllerBase
    {
        /// <summary>
        ///     Сервис моделей.
        /// </summary>
        private EventService DataEntityService { get; } = dataEntityService;

        /// <summary>
        ///     Получить список курсов.
        ///     Если идентификаторы не указаны, возвращается список со всеми курсами.
        ///     Иначе возвращается список с указанными курсами, либо пустой список.
        /// </summary>
        /// <param name="ids">Список идентификаторов.</param>
        /// <returns>Результат операции со списком курсов.</returns>
        [HttpGet]
        public async Task<ActionResult<List<Event>>> Get([FromQuery] List<int> ids)
        {
            return await DataEntityService.Get(DataEntityService.DataContext.Events, ids);
        }

        /// <summary>
        ///     Сохранить курсы.
        /// </summary>
        /// <param name="entities">Список курсов.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<Event> entities)
        {
            var status = await DataEntityService.Set(DataEntityService.DataContext.Events, entities);

            if (!status)
            {
                return BadRequest("No courses were saved!");
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
            var status = await DataEntityService.Remove(DataEntityService.DataContext.Events, ids);

            if (!status)
            {
                return BadRequest("No courses were deleted!");
            }

            return Ok();
        }
    }
}
