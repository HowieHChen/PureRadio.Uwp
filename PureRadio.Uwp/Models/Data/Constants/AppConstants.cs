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

        public static Dictionary<int, string> ProvinceIdDict = new Dictionary<int, string>()
        {
            {3, "北京"},
            {5, "天津"},
            {7, "河北"},
            {83, "上海"},
            {19, "山西"},
            {31, "内蒙古"},
            {44, "辽宁"},
            {59, "吉林"},
            {69, "黑龙江"},
            {85, "江苏"},
            {99, "浙江"},
            {111, "安徽"},
            {129, "福建"},
            {139, "江西"},
            {151, "山东"},
            {169, "河南"},
            {187, "湖北"},
            {202, "湖南"},
            {217, "广东"},
            {239, "广西"},
            {254, "海南"},
            {257, "重庆"},
            {259, "四川"},
            {281, "贵州"},
            {291, "云南"},
            {316, "陕西"},
            {327, "甘肃"},
            {351, "宁夏"},
            {357, "新疆"},
            {308, "西藏"},
            {342, "青海"},
        };
    }
}
