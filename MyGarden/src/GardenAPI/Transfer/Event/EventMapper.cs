namespace GardenAPI.Transfer.Event
{
    public static class EventMapper
    {
        public static Entities.Events.Event ToEntity(this RequestEventDTO requestEvent)
        {
            return new Entities.Events.Event
            {
                UserId = requestEvent.UserId,
                PlantId = requestEvent.PlantId,
                Title = requestEvent.Title,
                Date = requestEvent.Date
            };
        }


        public static EventDTO ToDTO(this Entities.Events.Event event_p)
        {
            return new EventDTO
            {
                Id = event_p.Id,
                UserId = event_p.UserId,
                PlantId = event_p.PlantId,
                Title = event_p.Title,
                Date = event_p.Date,
                CreatedAt = event_p.CreatedAt,
                UpdatedAt = event_p.UpdatedAt
            };
        }
    }
}

