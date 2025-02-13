﻿using GardenAPI.Data;
using GardenAPI.Entities.Plants;

namespace GardenAPI.Service.Plants
{
    public class PlantService(DataContext dataContext) : HasUserIdEntityService<Plant>(dataContext)
    {
    }
}
