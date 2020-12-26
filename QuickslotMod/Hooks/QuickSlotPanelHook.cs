using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace SwappableHotbar
{
    [HarmonyPatch(typeof(KeyboardQuickSlotPanel), "InitializeQuickSlotDisplays")]
    public class QuickSlotPanelHook
    {
        [HarmonyPrefix]
        public static void Prefix(KeyboardQuickSlotPanel __instance, ref QuickSlot.QuickSlotIDs[] ___DisplayOrder, QuickSlotDisplay[] ___m_quickSlotDisplays)
        {

            ___DisplayOrder = new QuickSlot.QuickSlotIDs[11 + SwappableHotbar.SLOTS_TO_ADD];
            for (int i = 0; i < ___DisplayOrder.Length; i++)
                ___DisplayOrder[i] = (QuickSlot.QuickSlotIDs)(i + 1);
        }

        [HarmonyPostfix]
        public static void Postfix(KeyboardQuickSlotPanel __instance, QuickSlotDisplay[] ___m_quickSlotDisplays)
        {
            SwappableHotbar.Hotbars.AddDisplays(___m_quickSlotDisplays);

            var layoutGroup = __instance.GetComponent<HorizontalLayoutGroup>();
            if(layoutGroup == null) // This is panel in settings
            {
                var grid = __instance.GetComponent<FlexibleGridLayoutGroup>();

                var sizeFitter = grid.gameObject.AddComponent<ContentSizeFitter>();
                sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

                var rect = grid.GetComponent<RectTransform>();
                var size = grid.cellSize;
                size.x = grid.cellSize.x * 6 + grid.spacing.x * 5 + grid.padding.left + grid.padding.right;
                rect.sizeDelta = size;

                rect.anchorMin = Vector2.one * 0.5f;
                rect.anchorMax = Vector2.one * 0.5f;
                rect.pivot = Vector2.one * 0.5f;
                rect.anchoredPosition = Vector2.zero;

                var leftDecor = grid.transform.Find("LeftDecoration");
                var rightDecor = grid.transform.Find("RightDecoration");
                leftDecor.GetComponent<RectTransform>().anchoredPosition -= Vector2.right * 70;
                rightDecor.GetComponent<RectTransform>().anchoredPosition -= Vector2.left * 70;
            }
            if (layoutGroup != null) // This is hotbar
            {

                GameObject.DestroyImmediate(layoutGroup);
                var grid = __instance.gameObject.AddComponent<GridLayoutGroup>();
                grid.cellSize = new Vector2(64, 96);
                grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                grid.constraintCount = 1;
                grid.spacing = new Vector2(6f, 20f);

                var sizeFitter = __instance.gameObject.AddComponent<ContentSizeFitter>();
                sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

                var rect = __instance.GetComponent<RectTransform>();
                rect.pivot = new Vector2(1, 0);
                rect.anchorMin = new Vector2(1, 0);
                rect.anchorMax = new Vector2(1, 0);
                rect.anchoredPosition = new Vector2(-28f, 14f);

                SwappableHotbar.CreateHotbarLayout(grid);
            }
        }
    }
}