using PureRadio.Uwp.Models.Local;
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
            public const string RefreshToken = "RefreshToken";
            public const string QingTingId = "QingTingId";
            public const string ExpireTime = "ExpireTime";
            public const string SavedVolumeState = "SavedVolumeState";
            public const string LocalRegionId = "LocalRegionId";
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

        public static List<RadioCategoryItem> RadioCategories = new List<RadioCategoryItem>()
        {
            new RadioCategoryItem(433,"ms-appx:///Assets/Image/RadioCategories/433.png"),
            new RadioCategoryItem(442,"ms-appx:///Assets/Image/RadioCategories/442.png"),
            new RadioCategoryItem(429,"ms-appx:///Assets/Image/RadioCategories/429.png"),
            new RadioCategoryItem(439,"ms-appx:///Assets/Image/RadioCategories/439.png"),
            new RadioCategoryItem(432,"ms-appx:///Assets/Image/RadioCategories/432.png"),
            new RadioCategoryItem(441,"ms-appx:///Assets/Image/RadioCategories/441.png"),
            new RadioCategoryItem(430,"ms-appx:///Assets/Image/RadioCategories/430.png"),
            new RadioCategoryItem(431,"ms-appx:///Assets/Image/RadioCategories/431.png"),
            new RadioCategoryItem(440,"ms-appx:///Assets/Image/RadioCategories/440.png"),
            new RadioCategoryItem(438,"ms-appx:///Assets/Image/RadioCategories/438.png"),
            new RadioCategoryItem(435,"ms-appx:///Assets/Image/RadioCategories/435.png"),
            new RadioCategoryItem(436,"ms-appx:///Assets/Image/RadioCategories/436.png"),
            new RadioCategoryItem(434,"ms-appx:///Assets/Image/RadioCategories/434.png"),
        };

        public static Dictionary<int, string> RadioCategoryDict = new Dictionary<int, string>()
        {
            {433, "LangRadioCategory433"},   // 资讯台
            {442, "LangRadioCategory442"},   // 音乐台
            {429, "LangRadioCategory429"},   // 交通台
            {439, "LangRadioCategory439"},   // 经济台
            {432, "LangRadioCategory432"},   // 文艺台
            {441, "LangRadioCategory441"},   // 都市台
            {430, "LangRadioCategory430"},   // 体育台
            {431, "LangRadioCategory431"},   // 双语台
            {440, "LangRadioCategory440"},   // 综合台
            {438, "LangRadioCategory438"},   // 生活台
            {435, "LangRadioCategory435"},   // 旅游台
            {436, "LangRadioCategory436"},   // 曲艺台
            {434, "LangRadioCategory434"}    // 方言台
        };

        public static Dictionary<int, string> ProvinceDict = new Dictionary<int, string>()
        {
            {3, "LangProvince3"},     // 北京
            {5, "LangProvince5"},     // 天津
            {7, "LangProvince7"},     // 河北
            {83, "LangProvince83"},   // 上海
            {19, "LangProvince19"},   // 山西
            {31, "LangProvince31"},  // 内蒙古
            {44, "LangProvince44"},   // 辽宁
            {59, "LangProvince59"},   // 吉林
            {69, "LangProvince69"},   // 黑龙江
            {85, "LangProvince85"},   // 江苏
            {99, "LangProvince99"},   // 浙江
            {111, "LangProvince111"}, // 安徽
            {129, "LangProvince129"}, // 福建
            {139, "LangProvince139"}, // 江西
            {151, "LangProvince151"}, // 山东
            {169, "LangProvince169"}, // 河南
            {187, "LangProvince187"}, // 湖北
            {202, "LangProvince202"}, // 湖南
            {217, "LangProvince217"}, // 广东
            {239, "LangProvince239"}, // 广西
            {254, "LangProvince254"}, // 海南
            {257, "LangProvince257"}, // 重庆
            {259, "LangProvince259"}, // 四川
            {281, "LangProvince281"}, // 贵州
            {291, "LangProvince291"}, // 云南
            {316, "LangProvince316"}, // 陕西
            {327, "LangProvince327"}, // 甘肃
            {351, "LangProvince351"}, // 宁夏
            {357, "LangProvince357"}, // 新疆
            {308, "LangProvince308"}, // 西藏
            {342, "LangProvince342"}, // 青海
        };

        public static Dictionary<string, int> ProvinceIdDict = new Dictionary<string, int>()
        {
            {"北京", 3},
            {"天津", 5},
            {"河北", 7},
            {"上海", 83},
            {"山西", 19},
            {"内蒙古", 31},
            {"辽宁", 44},
            {"吉林", 59},
            {"黑龙江", 69},
            {"江苏", 85},
            {"浙江", 99},
            {"安徽", 111},
            {"福建", 129},
            {"江西", 139},
            {"山东", 151},
            {"河南", 169},
            {"湖北", 187},
            {"湖南", 202},
            {"广东", 217},
            {"广西", 239},
            {"海南", 254},
            {"重庆", 257},
            {"四川", 259},
            {"贵州", 281},
            {"云南", 291},
            {"陕西", 316},
            {"甘肃", 327},
            {"宁夏", 351},
            {"新疆", 357},
            {"西藏", 308},
            {"青海", 342},
        };
    }
}
