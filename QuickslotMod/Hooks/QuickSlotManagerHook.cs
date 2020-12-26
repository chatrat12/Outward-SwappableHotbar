using HarmonyLib;
using UnityEngine;

namespace SwappableHotbar
{
    [HarmonyPatch(typeof(CharacterQuickSlotManager), "Awake")]
    public class QuickSlotManagerHook
    {
        [HarmonyPrefix]
        public static void Prefix(CharacterQuickSlotManager __instance, QuickSlot[] ___m_quickSlots)
        {
            var quickSlotTrans = __instance.transform.Find("QuickSlots");

            for(int i = 0; i < SwappableHotbar.SLOTS_TO_ADD; i++)
            {
                var go = new GameObject((i + 12).ToString(), typeof(QuickSlot));
                go.transform.SetParent(quickSlotTrans);
            }
        }
    }
}