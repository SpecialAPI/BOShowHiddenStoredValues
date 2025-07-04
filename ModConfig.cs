using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BOShowHiddenStoredValues
{
    public static class ModConfig
    {
        public static ConfigFile Config;

        public static ConfigEntry<bool> ForgottenAbilitiesDisplayEnabled;
        public static ConfigEntry<Color> ForgottenAbilitiesDisplayColor;
        public static ConfigEntry<bool> FleetingDisplayEnabled;
        public static ConfigEntry<Color> FleetingDisplayColor;
        public static ConfigEntry<bool> AbominationDisplayEnabled;
        public static ConfigEntry<Color> AbominationDisplayColor;

        public static void Init()
        {
            ForgottenAbilitiesDisplayEnabled = Config.Bind("HiddenStoredValueDisplay", "ForgottenAbilitiesDisplayEnabled", true, "Whether or not abilities forgotten by Forgetful should be displayed when hovering over enemies.");
            ForgottenAbilitiesDisplayColor = Config.Bind("HiddenStoredValueDisplay", "ForgottenAbilitiesDisplayColor", new Color(0f, 0.5961f, 0.8667f), "The color of the forgotten ability display.");

            FleetingDisplayEnabled = Config.Bind("HiddenStoredValueDisplay", "FleetingDisplayEnabled", true, "Whether or not the Fleeting turn count should be displayed when hovering over enemies.");
            FleetingDisplayColor = Config.Bind("HiddenStoredValueDisplay", "FleetingDisplayColor", new Color(0f, 0.5961f, 0.8667f), "The color of the Fleeting turn count display.");

            AbominationDisplayEnabled = Config.Bind("HiddenStoredValueDisplay", "AbominationDisplayEnabled", true, "Whether or not the Abomination extra ability count should be displayed when hovering over enemies.");
            AbominationDisplayColor = Config.Bind("HiddenStoredValueDisplay", "AbominationDisplayColor", new Color(0.8667f, 0f, 0.2157f), "The color of the Abomination extra ability count display.");
        }
    }
}
