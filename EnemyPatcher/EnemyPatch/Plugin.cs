using BepInEx;
using HarmonyLib;

namespace EnemyPitchPatch
{
    [BepInPlugin(
        "superkiesel.enemypitchpatch",
        "EnemyPitchPatch",
        "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private Harmony harmony;

        private void Awake()
        {
            EnemyPitchSettings.Initialize(Config);

            harmony = new Harmony("superkiesel.enemypitchpatch");
            harmony.PatchAll();

            Logger.LogInfo("EnemyPitchPatch loaded.");
        }

        private void OnDestroy()
        {
            harmony?.UnpatchSelf();
        }
    }
}
