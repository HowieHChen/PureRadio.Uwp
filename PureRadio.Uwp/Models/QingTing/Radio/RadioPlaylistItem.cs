using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Radio
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RadioPlaylistItem
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "start_time", Required = Required.Default)]
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "end_time", Required = Required.Default)]
        public string EndTime { get; set; }
        /// <summary>
        /// 节目时长
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "duration", Required = Required.Default)]
        public int Duration { get; set; }
        /// <summary>
        /// 星期几的节目(1->Sunday,2->Monday,etc.)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "day", Required = Required.Default)]
        public int Day { get; set; }
        /// <summary>
        /// 节目所属电台的ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "channel_id", Required = Required.Default)]
        public int RadioId { get; set; }
        /// <summary>
        /// 节目标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Default)]
        public string Title { get; set; }
        /// <summary>
        /// 主播(可能有多个)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "broadcasters", Required = Required.Default)]
        public List<BroadcastersItem> Broadcasters { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class BroadcastersItem
    {
        /// <summary>
        /// 主播Id(似乎无用)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public int BroadcasterId { get; set; }
        /// <summary>
        /// 主播用户名
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "username", Required = Required.Default)]
        public string UserName { get; set; }
        /// <summary>
        /// 微博头像
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "thumb", Required = Required.Default)]
        public string WeiboThumb { get; set; }
        /// <summary>
        /// 微博用户名
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "weibo_name", Required = Required.Default)]
        public string WeiboName { get; set; }
        /// <summary>
        /// 微博Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "weibo_id", Required = Required.Default)]
        public string WeiboId { get; set; }
    }

}
