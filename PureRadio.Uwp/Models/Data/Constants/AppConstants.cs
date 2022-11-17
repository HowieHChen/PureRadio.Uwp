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

        public static Dictionary<int, string> RadioCategoryDict = new Dictionary<int, string>()
        {
            {433, "LangRadioCategory433"},   //资讯台
            {442, "LangRadioCategory442"},   //音乐台
            {429, "LangRadioCategory429"},   //交通台
            {439, "LangRadioCategory439"},   //经济台
            {432, "LangRadioCategory432"},   //文艺台
            {441, "LangRadioCategory441"},   //都市台
            {430, "LangRadioCategory430"},   //体育台
            {431, "LangRadioCategory431"},   //双语台
            {440, "LangRadioCategory440"},   //综合台
            {438, "LangRadioCategory438"},   //生活台
            {435, "LangRadioCategory435"},   //旅游台
            {436, "LangRadioCategory436"},   //曲艺台
            {434, "LangRadioCategory434"}    //方言台
        };

        public static Dictionary<int, string> ContentCategoryDict = new Dictionary<int, string>()
        {
            {521, "小说"},    //
            {3251, "脱口秀"},  //
            {527, "相声小品"},  //
            {545, "头条"},    //
            {529, "情感"},    //
            {1599, "儿童"},   //
            {3636, "出版精品"}, //
            {531, "历史"},    //
            {3496, "评书"},   //
            {523, "音乐"},    //
            {533, "财经"},    //
            {537, "教育"},    //
            {547, "娱乐"},    //
            {3588, "影视"},   //
            {3613, "文化"},   //
            {543, "外语"},    //
            {3385, "汽车"},   //
            {535, "科技"},    //
            {3276, "戏曲"},   //
            {3442, "广播剧"},  //
            {3427, "二次元"},  //
            {1737, "校园"},   //
            {3600, "品牌电台"}, //
            {3637, "超级会员"}, //
            {3631, "联合专区"}, //
            {3670, "生活"},   //
            {3675, "母婴"}    //
        };
    }
}
