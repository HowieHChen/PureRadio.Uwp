using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Recommend
{
    /// <summary>
    /// 主页推荐响应
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class RecommendResponse
    {
        /// <summary>
        /// 主页推荐响应数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "data", Required = Required.Default)]
        public RecommendResponseData Data { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class RecommendResponseData
    {
        /// <summary>
        /// 主页推荐电台数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "radioPage", Required = Required.Default)]
        public RecommendRadioData RadioData { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class RecommendRadioData
    {
        /// <summary>
        /// 电台直播推荐(属于电台)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "radioPlaying", Required = Required.Default)]
        public List<RecommendRadioLiveItem> LivingRadios { get; set; }
        /// <summary>
        /// 电台回放推荐(属于内容/专辑)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "replayRadio", Required = Required.Default)]
        public List<RecommendRadioReplayItem> ReplayRadios { get; set; }
    }  
}
