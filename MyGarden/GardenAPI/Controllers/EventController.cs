﻿using GardenAPI.Entities.Events;
using GardenAPI.Service.Common;
using GardenAPI.Transfer.Event;
using GardenAPI.Transfer.Plant;
using Microsoft.AspNetCore.Mvc;

namespace GardenAPI.Controllers
{
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
        public async Task<ActionResult<IEnumerable<EventDTO>>> Get([FromQuery] List<int> ids)
        {
            return Ok((await DataEntityService.Get(DataEntityService.DataContext.Events, ids)).Select(x=>x.ToDTO()));
        }

        /// <summary>
        ///     Сохранить курсы.
        /// </summary>
        /// <param name="entities">Список курсов.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<RequestEventDTO> entities)
        {
            var status = await DataEntityService.Set(DataEntityService.DataContext.Events, entities.Select(x => x.ToEntity()).ToList());

            if (!status)
            {
                return BadRequest("No events were saved!");
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
                return BadRequest("No events were deleted!");
            }

            return Ok();
        }
    }
}
