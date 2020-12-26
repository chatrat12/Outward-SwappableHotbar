using SideLoader;

namespace SwappableHotbar
{
    public static class Input
    {
        public static void AddKeyBindings()
        {
            CustomKeybindings.AddAction("Swap Hotbar", KeybindingsCategory.QuickSlot, ControlType.Keyboard);
            for (int i = 8; i < 12; i++)
                CustomKeybindings.AddAction($"Quick Slot {i + 1}", KeybindingsCategory.QuickSlot, ControlType.Keyboard);
        }

        public static void UpdateInput(int playerID, Character character)
        {
            if (CustomKeybindings.GetKeyDown("Swap Hotbar"))
                SwappableHotbar.Hotbars.SwapBars();

            if (ControlsInput.QuickSlotInstant1(playerID))
                QuickSlotInput(0, character);
            else if (ControlsInput.QuickSlotInstant2(playerID))
                QuickSlotInput(1, character);
            else if (ControlsInput.QuickSlotInstant3(playerID))
                QuickSlotInput(2, character);
            else if (ControlsInput.QuickSlotInstant4(playerID))
                QuickSlotInput(3, character);
            else if (ControlsInput.QuickSlotInstant5(playerID))
                QuickSlotInput(4,character);
            else if (ControlsInput.QuickSlotInstant6(playerID))
                QuickSlotInput(5, character);
            else if (ControlsInput.QuickSlotInstant7(playerID))
                QuickSlotInput(6, character);
            else if (ControlsInput.QuickSlotInstant8(playerID))
                QuickSlotInput(7, character);
            else
            {
                for (int i = 8; i < 12; i++)
                {
                    if (CustomKeybindings.GetKeyDown($"Quick Slot {i + 1}"))
                    {
                        QuickSlotInput(i, character);
                        break;
                    }
                }
            }
        }

        private static void QuickSlotInput(int index, Character character)
            => SwappableHotbar.Hotbars.ActiveBar.QuickSlotInput(index, character);
    }
}
