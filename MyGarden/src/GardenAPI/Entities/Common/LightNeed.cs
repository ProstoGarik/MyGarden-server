﻿using GardenAPI.Data;
using GardenAPI.Entities;
using GardenAPI.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace MyGarden.Server.Entity.Common
{
    public class LightNeed : CommonEntity
    {
        //None,
        //Low,
        //Medium,
        //High

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


        /// <summary>
        ///     Конфигурация модели <see cref="LightNeed" />.
        /// </summary>
        /// <param name="configuration">Конфигурация базы данных.</param>
        internal class Configuration(BaseConfiguration configuration) : Configuration<LightNeed>(configuration)
        {
            /// <summary>
            ///     Задать конфигурацию для модели.
            /// </summary>
            /// <param name="builder">Набор интерфейсов настройки модели.</param>
            public override void Configure(EntityTypeBuilder<LightNeed> builder)
            {
                base.Configure(builder);
            }
        }

        #endregion

        public List<Plant> Plants { get; set; } = [];
    }
}
