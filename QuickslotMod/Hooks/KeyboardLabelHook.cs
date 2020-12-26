using HarmonyLib;

namespace SwappableHotbar
{
    [HarmonyPatch(typeof(ControlsInput), "GetQuickSlotName")]
    public class SlotNameHook
    {
        [HarmonyPrefix]
        public static void Prefix(ref QuickSlot.QuickSlotIDs _slotID)
        {
            if ((int)_slotID > 12)
                _slotID -= 12;
        }

        [HarmonyPostfix]
        public static void Postfix(ref string __result, QuickSlot.QuickSlotIDs _slotID)
        {
            if((int)_slotID > 8)
                __result = $"Quick Slot {(int)_slotID}";
        }
    }
}
