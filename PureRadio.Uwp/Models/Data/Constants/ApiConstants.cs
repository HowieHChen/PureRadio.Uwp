using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PureRadio.Uwp.Models.Data.Constants.ServiceConstants;
using Windows.UI.Xaml.Controls;

namespace PureRadio.Uwp.Models.Data.Constants
{
    public static class ApiConstants
    {
        public const string _webBase = "https://webbff.qingting.fm/www";
        public const string _userBase = "https://user.qingting.fm";

        /// <summary>
        /// 账户 API
        /// </summary>
        public static class Account
        {
            /// <summary>
            /// 登录.
            /// </summary>
            public const string Login = _userBase + "/u2/api/v4/user/login";
            /// <summary>
            /// 刷新令牌信息.
            /// </summary>
            public const string RefreshToken = _userBase + "/u2/api/v4/auth";
            /// <summary>
            /// 收藏夹.
            /// </summary>
            public const string FavCahnnel = _webBase + "/favchannel";
        }

        /// <summary>
        /// 搜索 API
        /// </summary>
        public static class Search
        {
            /// <summary>
            /// 搜索建议.
            /// </summary>
            public const string Suggest = "https://search.qtfm.cn/v3/suggest";
            /// <summary>
            /// 搜索电台
            /// </summary>
            public const string Radio = _webBase;
            /// <summary>
            /// 搜索内容(专辑)
            /// </summary>
            public const string Content = _webBase;
            /// <summary>
            /// 搜索请求字符串模板
            /// </summary>
            public const string SearchFormat = "{{\"query\":\"{{searchResultsPage(keyword:\\\"{0}\\\", page:{1}, include:\\\"{2}\\\" ) {{numFound,searchData}}}}\"}}";
            /// <summary>
            /// 搜索请求字符串模板关键字
            /// </summary>
            public const string ParamKeyword = "keyword";
            /// <summary>
            /// 搜索请求字符串模板页数
            /// </summary>
            public const string ParamPage = "page";
            /// <summary>
            /// 搜索请求字符串模板类型
            /// </summary>
            public const string ParamType = "type";
            /// <summary>
            /// 搜索请求类型为电台
            /// </summary>
            public const string TypeRadio = "channel_live";
            /// <summary>
            /// 搜素请求类型为内容(专辑)
            /// </summary>
            public const string TypeContent = "channel_ondemand";
        }

        /// <summary>
        /// 电台 API
        /// </summary>
        public static class Radio
        {
            /// <summary>
            /// 电台详情
            /// </summary>
            public const string Detail = "http://rapi.qingting.fm/channels/";
            /// <summary>
            /// 电台播放(直播)
            /// </summary>
            public const string Live = "http://ls.qingting.fm/live/";
            /// <summary>
            /// 电台播放(回放)
            /// </summary>
            public const string OnDemand = "https://lcache.qtfm.cn/cache/";
        }

        /// <summary>
        /// 有声内容(专辑) API
        /// </summary>
        public static class Content
        {
            /// <summary>
            /// 专辑详情
            /// </summary>
            public const string Detail = "https://webapi.qingting.fm/api/pc/channels/";
            /// <summary>
            /// 专辑播放列表
            /// </summary>
            public const string PList = "https://i.qingting.fm/capi/channel/";
            /// <summary>
            /// 专辑播放
            /// </summary>
            public const string Play = "https://audio.qtfm.cn/audiostream/redirect/";
        }

        /// <summary>
        /// 密钥
        /// </summary>
        public static class Key
        {
            /// <summary>
            /// 播放电台直播的密钥
            /// </summary>
            public const string Radio = "Lwrpu$K5oP";
            /// <summary>
            /// 播放专辑的密钥
            /// </summary>
            public const string Content = "fpMn12&38f_2e";
        }
    }
}
