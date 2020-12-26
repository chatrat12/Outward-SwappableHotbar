using HarmonyLib;

namespace SwappableHotbar
{
    // OnEnable throws an exception for some reason. Don't seem to need it for just keyboard :D
    [HarmonyPatch(typeof(QuickSlotDisplay), "OnEnable")]
    public class QuickSlotDisplayHack
    {
        [HarmonyPrefix]
        public static bool Prefix(QuickSlotDisplay __instance)
        {
            return false;
        }
    }
}
