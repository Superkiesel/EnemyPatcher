using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace EnemyPitchPatch
{
    [HarmonyPatch]
    public static class EnemyTimePatch
    {
        private static bool IsHost =>
            GameNetworkManager.Instance != null &&
            GameNetworkManager.Instance.isHostingGame;

        [HarmonyPatch(typeof(JesterAI), "SetJesterInitialValues")]
        [HarmonyPostfix]
        private static void JesterInitialValuesPostfix(JesterAI __instance)
        {
            if (!IsHost || __instance == null)
                return;

            float time = EnemyPitchSettings.JesterWindupTime.Value;

            if (time < 0f)
                return;

            __instance.popUpTimer = Mathf.Max(0.1f, time);
        }

        [HarmonyPatch(typeof(ClaySurgeonAI), "HourChanged")]
        [HarmonyPostfix]
        private static void BarberHourChangedPostfix(
            ClaySurgeonAI __instance)
        {
            if (!IsHost || __instance == null)
                return;

            float interval = EnemyPitchSettings.BarberStepInterval.Value;

            if (interval < 0f)
                return;

            interval = Mathf.Max(0.1f, interval);

            __instance.currentInterval = interval;
            __instance.startingInterval = interval;
            __instance.endingInterval = interval;
        }
    }

    [HarmonyPatch]
    public static class MaskedCutsceneTimePatch
    {
        private static MethodBase TargetMethod()
        {
            return AccessTools.EnumeratorMoveNext(
                AccessTools.Method(
                    typeof(MaskedPlayerEnemy),
                    "killAnimation"));
        }

        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(
            IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldc_R4 &&
                    instruction.operand is float value)
                {
                    if (value == 0.3f)
                    {
                        yield return new CodeInstruction(
                            OpCodes.Call,
                            AccessTools.Method(
                                typeof(MaskedCutsceneTimePatch),
                                nameof(GetApproachDuration)));

                        continue;
                    }

                    if (value == 1.2f)
                    {
                        yield return new CodeInstruction(
                            OpCodes.Call,
                            AccessTools.Method(
                                typeof(MaskedCutsceneTimePatch),
                                nameof(GetGlowDuration)));

                        continue;
                    }

                    if (value == 1.5f)
                    {
                        yield return new CodeInstruction(
                            OpCodes.Call,
                            AccessTools.Method(
                                typeof(MaskedCutsceneTimePatch),
                                nameof(GetInfectionDuration)));

                        continue;
                    }
                }

                yield return instruction;
            }
        }

        private static float GetApproachDuration()
        {
            float value =
                EnemyPitchSettings.MaskedApproachDuration.Value;

            return value == -1f ? 0.3f : Mathf.Max(0f, value);
        }

        private static float GetGlowDuration()
        {
            float value =
                EnemyPitchSettings.MaskedGlowDuration.Value;

            return value == -1f ? 1.2f : Mathf.Max(0f, value);
        }

        private static float GetInfectionDuration()
        {
            float value =
                EnemyPitchSettings.MaskedInfectionDuration.Value;

            return value == -1f ? 1.5f : Mathf.Max(0f, value);
        }
    }
}
