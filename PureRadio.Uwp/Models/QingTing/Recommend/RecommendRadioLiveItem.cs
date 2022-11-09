using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Recommend
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RecommendRadioLiveItem
    {
        /// <summary>
        /// 电台封面图片(URL)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "imgUrl", Required = Required.Default)]
        public string Cover { get; set; }
        /// <summary>
        /// 电台名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name", Required = Required.Default)]
        public string Name { get; set; }
        /// <summary>
        /// 电台标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "channelTitle", Required = Required.Default)]
        public string Title { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "category", Required = Required.Default)]
        public string Category { get; set; }
        /// <summary>
        /// 正在播放的节目
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "nowplaying", Required = Required.Default)]
        public Nowplaying Nowplaying { get; set; }
        /// <summary>
        /// 电台听众计数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "playcount", Required = Required.Default)]
        public int AudienceCount { get; set; }
        /// <summary>
        /// 跳转相对地址(包含电台Id)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "to", Required = Required.Default)]
        public string RelativeAddr { get; set; }
        /// <summary>
        /// 当前节目标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "desc", Required = Required.Default)]
        public string ProgramDescription { get; set; }
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
        /// 当前节目时间
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "desc2", Required = Required.Default)]
        public string TimeDescription { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Nowplaying
    {
        /// <summary>
        /// 节目的ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public int ProgramId { get; set; }
        /// <summary>
        /// 节目时长
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "duration", Required = Required.Default)]
        public int Duration { get; set; }
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
        /// 节目名
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name", Required = Required.Default)]
        public string Name { get; set; }
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
    }
}
