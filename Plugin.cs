using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Reflection;
using Tools;
using UnityEngine;

namespace BOShowHiddenStoredValues
{
    [BepInPlugin(MOD_GUID, MOD_NAME, MOD_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string MOD_GUID = "SpecialAPI.ShowHiddenStoredValues";
        public const string MOD_NAME = "Show Hidden Stored Values";
        public const string MOD_VERSION = "1.0.0";
        public const string MOD_PREFIX = "ShowHiddenStoredValues";

        public const string FORGOTTEN_AB_LOCA = $"{MOD_PREFIX}_ForgottenAbilities";
        public const string FORGOTTEN_AB_DEFAULT = "Forgotten Abilities: {0}";
        public const string FLEETING_AB_LOCA = $"{MOD_PREFIX}_Fleeting";
        public const string FLEETING_AB_DEFAULT = "Fleeting: {0}";
        public const string ABOMINATION_AB_LOCA = $"{MOD_PREFIX}_Abomination";
        public const string ABOMINATION_AB_DEFAULT = "Abomination: {0}";

        public void Awake()
        {
            var harmony = new Harmony(MOD_GUID);
            harmony.PatchAll();

            ModConfig.Config = Config;
            ModConfig.Init();
        }
    }
}
