using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Radio
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RadioRecommendItem
    {
        /// <summary>
        /// 电台封面图片(URL)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "imgUrl", Required = Required.Default)]
        public string Cover { get; set; }
        /// <summary>
        /// 电台标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name", Required = Required.Default)]
        public string Title { get; set; }
        /// <summary>
        /// 当前节目开始时间
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "startTime", Required = Required.Default)]
        public string StartTime { get; set; }
        /// <summary>
        /// 当前节目结束时间
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "endTime", Required = Required.Default)]
        public string EndTime { get; set; }
        /// <summary>
        /// 正在播放的节目
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "desc", Required = Required.Default)]
        public string Nowplaying { get; set; }
        /// <summary>
        /// 电台ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public int RadioId { get; set; }
    }
}
