namespace GardenAPI.Transfer.Plant
{
    public static class PlantMapper
    {
        public static Entities.Plants.Plant ToEntity(this RequestPlantDTO requestPlant)
        {
            return new Entities.Plants.Plant
            {
                Id = requestPlant.Id,
                UserId = requestPlant.UserId,
                Title = requestPlant.Title,
                BiologyTitle = requestPlant.BiologyTitle,
                GroupId = requestPlant.GroupId,
                WateringNeedId = requestPlant.WateringNeedId,
                LightNeedId = requestPlant.LightNeedId,
                StageId = requestPlant.StageId,
                ImageId = requestPlant.ImageId,
                RipeningPeriod = requestPlant.RipeningPeriod,
                Fertilization = requestPlant.Fertilization,
                Toxicity = requestPlant.Toxicity,
                Replacing = requestPlant.Replacing,
                Description = requestPlant.Description
            };
        }


        public static PlantDTO ToDTO(this Entities.Plants.Plant plant)
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

