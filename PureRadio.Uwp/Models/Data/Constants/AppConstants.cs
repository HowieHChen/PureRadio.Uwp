using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Constants
{
    public static class AppConstants
    {
        public static class SettingsKey
        {
            public const string ConfigTheme = "ConfigTheme";
            public const string ConfigLanguage = "ConfigLanguage";
            public const string AccountOnline = "AccountOnline";
            public const string AccountPhone = "AccountPhone";
            public const string AccountPassword = "AccountPassword";
            public const string SavedVolumeState = "SavedVolumeState";
        }

        public static class SettingsValue
        {
            public const string Light = "light";
            public const string Dark = "dark";
            public const string Default = "default";
            public const string Auto = "auto";
            public const string ZH_CN = "zh-cn";
            public const string EN_US = "en-us";
        }
    }
}
