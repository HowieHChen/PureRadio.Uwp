using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Recommend
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RecommendRadioReplayItem
    {
        /// <summary>
        /// 电台节目封面图片(URL)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "imgUrl", Required = Required.Default)]
        public string Cover { get; set; }
        /// <summary>
        /// 电台节目标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name", Required = Required.Default)]
        public string Title { get; set; }
        /// <summary>
        /// 分类(可能为空)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "category", Required = Required.Default)]
        public string Category { get; set; }
        /// <summary>
        /// 电台节目播放量
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "playcount", Required = Required.Default)]
        public int Playcount { get; set; }
        /// <summary>
        /// 跳转相对地址(包含专辑Id)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "to", Required = Required.Default)]
        public string RelativeAddr { get; set; }
        /// <summary>
        /// 电台节目所属电台的标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "desc2", Required = Required.Default)]
        public string RadioTitle { get; set; }
    }
}
