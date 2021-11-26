/**
 * Kronometer - Makes the KSP clock easily editable
 * Copyright (c) 2017 Sigma88, Thomas P.
 * Licensed under the Terms of the MIT License
 */

using System;
using System.Text;
using UnityEngine;
using Kopernicus.ConfigParser;

namespace Kronometer
{
    // KRONOMETER STARTS HERE
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class Kronometer : MonoBehaviour
    {
        /// <summary>
        /// Name of the config node group which manages Kronometer
        /// </summary>
        public const string rootNodeName = "Kronometer";

        void Start()
        {
            // Get the configNode
            ConfigNode kronometer = GameDatabase.Instance.GetConfigs(rootNodeName)[0].config;

            // Parse the config node
            SettingsLoader loader = Parser.CreateObjectFromConfigNode<SettingsLoader>(kronometer);

            if  // Make sure we need the clock and all values are defined properly
            (
                double.PositiveInfinity > loader.Clock.hour.value &&
                loader.Clock.hour.value > loader.Clock.minute.value &&
                loader.Clock.minute.value > loader.Clock.second.value &&
                loader.Clock.second.value > 0
            )
            {
                // Find the home planet
                CelestialBody homePlanet = FlightGlobals.GetHomeBody();

                // Get home planet day (rotation) and year (revolution)
                if (loader.useHomeDay)
                    loader.Clock.day.value = homePlanet.solarDayLength;
                if (loader.useHomeYear)
                    loader.Clock.year.value = homePlanet.orbitDriver.orbit.period;

                // If tidally locked set day = year
                if (loader.Clock.year.value == homePlanet.rotationPeriod)
                    loader.Clock.day.value = loader.Clock.year.value;

                // Convert negative numbers to positive
                loader.Clock.year.value = Math.Abs(loader.Clock.year.value);
                loader.Clock.day.value = Math.Abs(loader.Clock.day.value);

                // Fix Months
                if (loader.resetMonthNum == 0)
                    loader.resetMonthNum = loader.calendar?.Count ?? 1;

                // Round values where it is required
                if (loader.Clock.year.round)
                    loader.Clock.year.value = Math.Round(loader.Clock.year.value, 0);
                if (loader.Clock.day.round)
                    loader.Clock.day.value = Math.Round(loader.Clock.day.value, 0);
                if (loader.Clock.hour.round)
                    loader.Clock.hour.value = Math.Round(loader.Clock.hour.value, 0);
                if (loader.Clock.minute.round)
                    loader.Clock.minute.value = Math.Round(loader.Clock.minute.value, 0);
                if (loader.Clock.second.round)
                    loader.Clock.second.value = Math.Round(loader.Clock.second.value, 0);

                if  // Make sure we still need the clock and all values are still defined properly
                (
                    double.PositiveInfinity > loader.Clock.hour.value &&
                    loader.Clock.hour.value > loader.Clock.minute.value &&
                    loader.Clock.minute.value > loader.Clock.second.value &&
                    loader.Clock.second.value > 0
                )
                {
                    // Replace the stock Formatter
                    KSPUtil.dateTimeFormatter = new ClockFormatter(loader);
                }
            }
        }
    }
}
