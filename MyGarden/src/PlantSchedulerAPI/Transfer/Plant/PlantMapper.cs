namespace GardenAPI.Transfer.Plant
{
    public static class PlantMapper
    {
        public static Entities.Plant ToEntity(this RequestPlantDTO request_plant)
        {
            return new Entities.Plant
            {
                Id = request_plant.Id,
                UserId = request_plant.UserId,
                Title = request_plant.Title,
                BiologyTitle = request_plant.BiologyTitle,
                GroupId = request_plant.GroupId,
                WateringNeedId = request_plant.WateringNeedId,
                LightNeedId = request_plant.LightNeedId,
                StageId = request_plant.StageId,
                ImageId = request_plant.ImageId,
                RipeningPeriod = request_plant.RipeningPeriod,
                Fertilization = request_plant.Fertilization,
                Toxicity = request_plant.Toxicity,
                Replacing = request_plant.Replacing,
                Description = request_plant.Description
            };
        }


        public static PlantDTO ToDTO(this Entities.Plant plant)
        {
            return new PlantDTO
            {
                Id = plant.Id,
                Title = plant.Title,
                BiologyTitle = plant.BiologyTitle,
                GroupId = plant.GroupId,
                UserId = plant.UserId,
                WateringNeedId = plant.WateringNeedId,
                LightNeedId = plant.LightNeedId,
                StageId = plant.StageId,
                ImageId = plant.ImageId,
                RipeningPeriod = plant.RipeningPeriod,
                Fertilization = plant.Fertilization,
                Toxicity = plant.Toxicity,
                Replacing = plant.Replacing,
                Description = plant.Description,
                CreatedAt = plant.CreatedAt,
                UpdatedAt = plant.UpdatedAt
            };
        }
    }
}

