using SharedModConfig;
using System.Collections.Generic;

namespace SwappableHotbar
{
    public static class ConfigSettings
    {
        public const string SETTING_NAME_ROWS = "Number of Rows";
        public const string SETTING_NAME_CENTER = "Centered";
        public const string SETTING_NAME_XOFFSET = "Horizontal Offset";
        public const string SETTING_NAME_YOFFSET = "Vertical Offset";
        public const string SETTING_NAME_XSPACING = "Horizontal Spacing";
        public const string SETTING_NAME_YSPACING = "Vertical Spacing";

        public static ModConfig Create()
        {
            return new ModConfig()
            {
                ModName = SwappableHotbar.NAME,
                SettingsVersion = 0.1,
                Settings = new List<BBSetting>()
                {
                    new FloatSetting
                    {
                        SectionTitle = "Hotbar Options",
                        Name = SETTING_NAME_ROWS,
                        DefaultValue = 1f,
                        Increment = 1,
                        RoundTo = 0,
                        MinValue = 1,
                        MaxValue = 4,
                        ShowPercent = false,
                    },
                    new BoolSetting
                    {
                        Name = SETTING_NAME_CENTER,
                        DefaultValue = false,
                    },
                    new FloatSetting
                    {
                        Name = SETTING_NAME_XOFFSET,
                        DefaultValue = 0f,
                        Increment = 10,
                        RoundTo = 0,
                        MinValue = -200,
                        MaxValue = 200,
                        ShowPercent = false
                    },
                    new FloatSetting
                    {
                        Name = SETTING_NAME_YOFFSET,
                        DefaultValue = 0f,
                        Increment = 10,
                        RoundTo = 0,
                        MinValue = 0,
                        MaxValue = 200,
                        ShowPercent = false
                    },
                    new FloatSetting
                    {
                        Name = SETTING_NAME_XSPACING,
                        DefaultValue = 6f,
                        Increment = 2,
                        RoundTo = 0,
                        MinValue = 0,
                        MaxValue = 60,
                        ShowPercent = false
                    },
                    new FloatSetting
                    {
                        Name = SETTING_NAME_YSPACING,
                        DefaultValue = 20f,
                        Increment = 2,
                        RoundTo = 0,
                        MinValue = 0,
                        MaxValue = 60,
                        ShowPercent = false
                    }
                }
            };
        }
    }
}