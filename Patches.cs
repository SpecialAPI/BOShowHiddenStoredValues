using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace BOShowHiddenStoredValues
{
    [HarmonyPatch]
    public static class Patches
    {
        public static MethodInfo dfa_d = AccessTools.Method(typeof(Patches), nameof(DisplayForgottenAbilities_Display));

        [HarmonyPatch(typeof(CombatVisualizationController), nameof(CombatVisualizationController.ShowcaseEnemyTooltip))]
        [HarmonyILManipulator]
        public static void DisplayForgottenAbilities_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpBeforeNext(x => x.MatchStloc(2)))
                return;

            crs.Emit(OpCodes.Ldloc_0);
            crs.Emit(OpCodes.Call, dfa_d);
        }

        public static string DisplayForgottenAbilities_Display(string orig, EnemyCombatUIInfo info)
        {
            if (!ModConfig.ForgottenAbilitiesDisplayEnabled.Value)
                return orig;

            if (orig == null || info == null)
                return orig;

            if (CombatManager.Instance._stats is not CombatStats cs)
                return orig;

            if (cs.Enemies is not Dictionary<int, EnemyCombat> enm || !enm.TryGetValue(info.ID, out var ec))
                return orig;

            if (ec == null || ec.ForgottenAbilityIDs is not HashSet<string> forgottenAbIds || ec.Abilities is not List<CombatAbility> abilities)
                return orig;

            if (forgottenAbIds.Count <= 0 || abilities.Count <= 0)
                return orig;

            var forgottenAbNames = new List<string>();
            foreach (var abId in forgottenAbIds)
            {
                if (string.IsNullOrEmpty(abId))
                    continue;

                foreach (var a in abilities)
                {
                    if (a == null || a.ability == null)
                        continue;

                    if (a.ability.name != abId)
                        continue;

                    forgottenAbNames.Add(a.ability.GetAbilityLocData().text);
                    break;
                }
            }

            if (forgottenAbNames.Count <= 0)
                return orig;

            var abString = string.Join(", ", forgottenAbNames);
            var forgottenAbString = FormatStoredValue(Plugin.FORGOTTEN_AB_LOCA, Plugin.FORGOTTEN_AB_DEFAULT, abString, ModConfig.ForgottenAbilitiesDisplayColor.Value);

            return $"{forgottenAbString}\n{orig}";
        }

        [HarmonyPatch(typeof(UnitStoreData_BasicSO), nameof(UnitStoreData_BasicSO.TryGetUnitStoreDataToolTip))]
        [HarmonyPostfix]
        public static void DisplayHiddenStoreData_Postfix(UnitStoreData_BasicSO __instance, ref bool __result, UnitStoreDataHolder holder, ref string result)
        {
            if (!string.IsNullOrEmpty(result))
                return;

            result = __instance._UnitStoreDataID switch
            {
                nameof(UnitStoredValueNames_GameIDs.FleetingPA) => FormatIntStoredValue(ModConfig.FleetingDisplayEnabled.Value, Plugin.FLEETING_AB_LOCA, Plugin.FLEETING_AB_DEFAULT, holder.m_MainData, false, ModConfig.FleetingDisplayColor.Value),
                nameof(UnitStoredValueNames_GameIDs.AbominationPA) => FormatIntStoredValue(ModConfig.AbominationDisplayEnabled.Value, Plugin.ABOMINATION_AB_LOCA, Plugin.ABOMINATION_AB_DEFAULT, holder.m_MainData, true, ModConfig.AbominationDisplayColor.Value),

                _ => string.Empty
            };
            __result = !string.IsNullOrEmpty(result);
        }

        [HarmonyPatch(typeof(UnitStoreData_ModIntSO), nameof(UnitStoreData_ModIntSO.TryGetUnitStoreDataToolTip))]
        [HarmonyPostfix]
        public static void HandleModStoredValueInteractions_Postfix(UnitStoreData_ModIntSO __instance, ref bool __result, UnitStoreDataHolder holder, ref string result)
        {
            const string HellIslandFellConsistentFleetingStoredValue = "ConsistentFleetingStoredValue";

            if (__instance._UnitStoreDataID == UnitStoredValueNames_GameIDs.FleetingPA.ToString())
            {
                if (!ModConfig.OverrideSaltEnemiesFleetingDisplay.Value)
                    return;

                result = FormatIntStoredValue(ModConfig.FleetingDisplayEnabled.Value, Plugin.FLEETING_AB_LOCA, Plugin.FLEETING_AB_DEFAULT, holder.m_MainData, false, ModConfig.FleetingDisplayColor.Value);
                __result = !string.IsNullOrEmpty(result);
            }
            else if(__instance._UnitStoreDataID == HellIslandFellConsistentFleetingStoredValue)
            {
                // Hell Island Fell's "consistent fleeting" starts at 1 on combat start and triggers when it reaches its stated value + 1.
                var realFleetingTurn = Mathf.Max(holder.m_MainData - 1, 0);

                result = FormatIntStoredValue(ModConfig.FleetingDisplayEnabled.Value, Plugin.FLEETING_AB_LOCA, Plugin.FLEETING_AB_DEFAULT, realFleetingTurn, false, ModConfig.FleetingDisplayColor.Value);
                __result = !string.IsNullOrEmpty(result);
            }
        }

        public static string FormatIntStoredValue(bool enabled, string locId, string locDefault, int data, bool needsNonZero, Color color)
        {
            if (!enabled)
                return string.Empty;

            if(data == 0 && needsNonZero)
                return string.Empty;

            return FormatStoredValue(locId, locDefault, data.ToString(), color);
        }

        public static string FormatStoredValue(string locId, string locDefault, string value, Color color)
        {
            var format = CustomLoc.GetUIData(locId, locDefault);
            var formattedValue = string.Format(format, value);
            var colorString = ColorUtility.ToHtmlStringRGB(color);

            return $"<color=#{colorString}>{formattedValue}</color>";
        }
    }
}
