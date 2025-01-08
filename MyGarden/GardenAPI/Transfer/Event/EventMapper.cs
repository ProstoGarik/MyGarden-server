namespace GardenAPI.Transfer.Event
{
    public static class EventMapper
    {
        public static Entities.Events.Event ToEntity(this RequestEventDTO request_event)
        {
            return new Entities.Events.Event
            {
                UserId = request_event.UserId,
                PlantId = request_event.PlantId,
                Title = request_event.Title,
                Date = request_event.Date
            };
        }


        public static EventDTO ToDTO(this Entities.Events.Event game)
        {
            return new EventDTO
            {
                Id = game.Id,
                UserId = game.UserId,
                PlantId = game.PlantId,
                Title = game.Title,
                Date = game.Date,
                CreatedAt = game.CreatedAt,
                UpdatedAt = game.UpdatedAt
            };
        }
    }
}

