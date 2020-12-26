using BepInEx;
using HarmonyLib;
using SharedModConfig;
using UnityEngine.UI;

namespace SwappableHotbar
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class SwappableHotbar : BaseUnityPlugin
    {
        public const string GUID = "com.Programmatic.SwappableHotbar";
        public const string NAME = "Swappable Hotbar";
        public const string VERSION = "0.1";

        public static ModConfig Settings { get; } = ConfigSettings.Create();
        public static Hotbars Hotbars { get; } = new Hotbars();
        public static HotbarLayout HotbarLayout { get; private set; }
        public const int EXISTING_SLOTS_COUNT = 11;
        public const int SLOTS_TO_ADD = Hotbars.HOTBAR_LENGTH * 2 - EXISTING_SLOTS_COUNT;


        private void Awake()
        {
            Settings.Register();
            Settings.OnSettingsSaved += () =>
            {
                if (HotbarLayout != null)
                    HotbarLayout.ApplySettings(Settings);
            };

            Input.AddKeyBindings();
            new Harmony(GUID).PatchAll();
        }

        public static void CreateHotbarLayout(GridLayoutGroup grid)
        {
            HotbarLayout = new HotbarLayout(grid);
            HotbarLayout.ApplySettings(Settings);
        }
    }
}
