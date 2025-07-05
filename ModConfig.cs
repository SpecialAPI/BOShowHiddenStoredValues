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

        public static ConfigEntry<bool> OverrideSaltEnemiesFleetingDisplay;

        public static void Init()
        {
            ForgottenAbilitiesDisplayEnabled = Config.Bind("HiddenStoredValueDisplay", "ForgottenAbilitiesDisplayEnabled", true, "Whether or not abilities forgotten by Forgetful should be displayed when hovering over enemies.");
            ForgottenAbilitiesDisplayColor = Config.Bind("HiddenStoredValueDisplay", "ForgottenAbilitiesDisplayColor", new Color(0f, 0.5961f, 0.8667f), "The color of the forgotten ability display.");

            FleetingDisplayEnabled = Config.Bind("HiddenStoredValueDisplay", "FleetingDisplayEnabled", true, "Whether or not the Fleeting turn count should be displayed when hovering over enemies.");
            FleetingDisplayColor = Config.Bind("HiddenStoredValueDisplay", "FleetingDisplayColor", new Color(0f, 0.5961f, 0.8667f), "The color of the Fleeting turn count display.");

            AbominationDisplayEnabled = Config.Bind("HiddenStoredValueDisplay", "AbominationDisplayEnabled", true, "Whether or not the Abomination extra ability count should be displayed when hovering over enemies.");
            AbominationDisplayColor = Config.Bind("HiddenStoredValueDisplay", "AbominationDisplayColor", new Color(0.8667f, 0f, 0.2157f), "The color of the Abomination extra ability count display.");

            OverrideSaltEnemiesFleetingDisplay = Config.Bind("ModCompatibility", "OverrideSaltEnemiesFleetingDisplay", true, "Whether or not this mod's Fleeting display should override the Fleeting display added by Salt Enemies.\nIf this is false, this mod's Fleeting display will be overriden by Salt Enemies' Fleeting display, even if FleetingDisplayEnabled is false.\nNote: if this is true, FleetingDisplayEnabled is false and Salt Enemies is enabled, neither of the mods will display the turn counter for Fleeting.");
        }
    }
}
