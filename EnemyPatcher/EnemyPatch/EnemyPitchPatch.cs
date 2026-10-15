using HarmonyLib;
using UnityEngine;

namespace EnemyPitchPatch
{
    [HarmonyPatch]
    public static class EnemyPitchPatch
    {
        [HarmonyPatch(typeof(EnemyAI), "Start")]
        [HarmonyPostfix]
        private static void EnemyStartPostfix(EnemyAI __instance)
        {
            ApplyPitch(__instance);
        }

        [HarmonyPatch(typeof(EnemyAI), "Update")]
        [HarmonyPostfix]
        private static void EnemyUpdatePostfix(EnemyAI __instance)
        {
            ApplyPitch(__instance);
        }

        private static void ApplyPitch(EnemyAI enemy)
        {
            if (enemy == null)
                return;

            float pitch = GetPitch(enemy);

            // -1 keeps the original game pitch.
            if (pitch == -1f)
                return;

            AudioSource[] sources =
                enemy.GetComponentsInChildren<AudioSource>(true);

            foreach (AudioSource source in sources)
            {
                if (source != null &&
                    !Mathf.Approximately(source.pitch, pitch))
                {
                    source.pitch = pitch;
                }
            }
        }

        private static float GetPitch(EnemyAI enemy)
        {
            switch (enemy.GetType().Name)
            {
                case "HoarderBugAI":
                    return EnemyPitchSettings.HoarderBug.Value;
                case "FlowermanAI":
                    return EnemyPitchSettings.Bracken.Value;
                case "SpringManAI":
                    return EnemyPitchSettings.CoilHead.Value;
                case "JesterAI":
                    return EnemyPitchSettings.Jester.Value;
                case "GirlAI":
                    return EnemyPitchSettings.GhostGirl.Value;
                case "MaskedPlayerEnemy":
                    return EnemyPitchSettings.Masked.Value;
                case "ClaySurgeonAI":
                    return EnemyPitchSettings.Barber.Value;
                case "NutcrackerEnemyAI":
                    return EnemyPitchSettings.Nutcracker.Value;
                case "CrawlerAI":
                    return EnemyPitchSettings.Thumper.Value;
                case "SpiderAI":
                case "SandSpiderAI":
                    return EnemyPitchSettings.Spider.Value;
                case "CentipedeAI":
                    return EnemyPitchSettings.SnareFlea.Value;
                case "ForestGiantAI":
                    return EnemyPitchSettings.ForestKeeper.Value;
                case "MouthDogAI":
                    return EnemyPitchSettings.EyelessDog.Value;
                case "BaboonBirdAI":
                    return EnemyPitchSettings.BaboonHawk.Value;
                case "BushWolfEnemy":
                    return EnemyPitchSettings.KidnapperFox.Value;
                case "ButlerEnemyAI":
                    return EnemyPitchSettings.Butler.Value;
                case "DoublewingAI":
                    return EnemyPitchSettings.TulipSnake.Value;
                case "DocileLocustBeesAI":
                    return EnemyPitchSettings.Locusts.Value;
                default:
                    return -1f;
            }
        }
    }
}
