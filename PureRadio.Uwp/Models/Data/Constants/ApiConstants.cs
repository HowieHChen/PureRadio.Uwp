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
        /// 网络 API
        /// </summary>
        public static class Network
        {
            /// <summary>
            /// 获取IP地址
            /// </summary>
            public const string IpAddr = "https://ip.qtfm.cn";
        }

        /// <summary>
        /// 首页推荐 API
        /// </summary>
        public static class Recommend
        {
            /// <summary>
            /// 电台推荐
            /// </summary>
            public const string Radio = _webBase;
            /// <summary>
            /// 电台推荐请求内容
            /// </summary>
            public const string RequestContent = "{\"query\":\"{radioPage {radioPlaying,replayRadio}}\"}";
        }

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
            public const string Live = "http://ls.qingting.fm/live/{0}/64k.m3u8";
            /// <summary>
            /// 电台播放(回放) https://lcache.qtfm.cn/cache/{年月日:20220531}/{广播电台id}/{广播电台id}_{年月日}_{开始时间}_{结束时间}_24_0.aac
            /// </summary>
            public const string OnDemand = "https://lcache.qtfm.cn/cache/{0}/{1}/{2}_{3}_{4}_{5}_24_0.aac";
            /// <summary>
            /// 根据分类请求电台
            /// </summary>
            public const string Category = "http://rapi.qingting.fm/channels";
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
            public const string Play = "https://audio.qtfm.cn/audiostream/redirect/{0}/{1}";
            /// <summary>
            /// 根据分类请求专辑
            /// </summary>
            public const string Category = "https://i.qingting.fm/capi/neo-channel-filter";
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
