﻿namespace GardenAPI.Transfer.Common
{
    public record CommonDTO:IdentifiableEntityDTO
    {
        public string? Title { get; init; }
    }
}
