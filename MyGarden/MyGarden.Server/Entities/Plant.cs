﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyGarden.Server.Data.Configuration;
using MyGarden.Server.Entity.Common;
using MyGarden.Server.Entity.Events;

namespace MyGarden.Server.Entities
{
    public class Plant : IdentifiableEntity
    {

        /*                   __ _                       _   _
        *   ___ ___  _ __  / _(_) __ _ _   _ _ __ __ _| |_(_) ___  _ __
        *  / __/ _ \| '_ \| |_| |/ _` | | | | '__/ _` | __| |/ _ \| '_ \
        * | (_| (_) | | | |  _| | (_| | |_| | | | (_| | |_| | (_) | | | |
        *  \___\___/|_| |_|_| |_|\__, |\__,_|_|  \__,_|\__|_|\___/|_| |_|
        *                        |___/
        * Константы, задающие базовые конфигурации полей
        * и ограничения модели.
        */

        #region Configuration

        public const int TitleLengthMax = 256;
        public const int BiologyTitleLengthMax = 256;
        public const int FertilizationLengthMax = 10240;
        public const int ToxicityLengthMax = 10240;
        public const int ReplacingLengthMax = 10240;
        public const int DescriptionLengthMax = 10240;

        public const bool IsTitleRequired = true;
        public const bool IsBiologyTitleRequired = true;
        public const bool IsGroupIdRequired = true;
        public const bool IsWateringNeedIdRequired = true;
        public const bool IsLightNeedIdRequired = true;
        public const bool IsStageIdRequired = true;
        public const bool IsImageIdRequired = false;
        public const bool IsRipeningPeriodRequired = false;
        public const bool IsFertilizationRequired = false;
        public const bool IsToxicityRequired = false;
        public const bool IsReplacingRequired = false;
        public const bool IsDescriptionRequired = false;

        /// <summary>
        ///     Конфигурация модели <see cref="Subject" />.
        /// </summary>
        /// <param name="configuration">Конфигурация базы данных.</param>
        internal class Configuration(ContextConfiguration configuration) : Configuration<Plant>(configuration)
        {
            /// <summary>
            ///     Задать конфигурацию для модели.
            /// </summary>
            /// <param name="builder">Набор интерфейсов настройки модели.</param>
            public override void Configure(EntityTypeBuilder<Plant> builder)
            {
                builder.HasOne(plant => plant.WateringNeed)
                    .WithMany(wateringNeed => wateringNeed.Plants)
                    .HasForeignKey(plant => plant.WateringNeedId)
                    .IsRequired(IsWateringNeedIdRequired);

                builder.HasOne(plant => plant.LightNeed)
                    .WithMany(lightNeed => lightNeed.Plants)
                    .HasForeignKey(plant => plant.LightNeedId)
                    .IsRequired(IsLightNeedIdRequired);

                builder.HasOne(plant => plant.Stage)
                    .WithMany(stage => stage.Plants)
                    .HasForeignKey(plant => plant.StageId)
                    .IsRequired(IsStageIdRequired);

                builder.HasOne(plant => plant.Group)
                    .WithMany(group => group.Plants)
                    .HasForeignKey(plant => plant.GroupId)
                    .IsRequired(IsGroupIdRequired);

                builder.Property(subject => subject.ImageId)
                    .IsRequired(IsImageIdRequired);

                builder.Property(subject => subject.RipeningPeriod)
                    .IsRequired(IsRipeningPeriodRequired);

                builder.Property(subject => subject.Title)
                    .HasMaxLength(TitleLengthMax)
                    .IsRequired(IsTitleRequired);

                builder.Property(subject => subject.BiologyTitle)
                    .HasMaxLength(BiologyTitleLengthMax)
                    .IsRequired(IsBiologyTitleRequired);

                builder.Property(subject => subject.Fertilization)
                    .HasMaxLength(FertilizationLengthMax)
                    .IsRequired(IsFertilizationRequired);

                builder.Property(subject => subject.Toxicity)
                    .HasMaxLength(ToxicityLengthMax)
                    .IsRequired(IsToxicityRequired);

                builder.Property(subject => subject.Replacing)
                        .HasMaxLength(ReplacingLengthMax)
                    .IsRequired(IsReplacingRequired);

                builder.Property(subject => subject.Description)
                    .HasMaxLength(DescriptionLengthMax)
                    .IsRequired(IsDescriptionRequired);


                base.Configure(builder);
            }
        }

        #endregion


        public required int GroupId { get; set; }
        public int WateringNeedId { get; set; }
        public int LightNeedId { get; set; }
        public int StageId { get; set; }

        public Group? Group { get; set; }
        public WateringNeed? WateringNeed { get; set; }
        public LightNeed? LightNeed { get; set; }
        public GrowStage? Stage { get; set; }

        public int ImageId { get; set; }
        public int RipeningPeriod { get; set; }

        public string? Title { get; set; }
        public string? BiologyTitle { get; set; }
        public string? Fertilization { get; set; }
        public string? Toxicity { get; set; }
        public string? Replacing { get; set; }
        public string? Description { get; set; }

        public List<Event> Events { get; set; } = [];


    }
}
