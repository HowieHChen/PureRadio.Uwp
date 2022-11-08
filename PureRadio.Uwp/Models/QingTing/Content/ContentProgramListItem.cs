using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Content
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ContentProgramListItem
    {
        /// <summary>
        /// 节目Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public int ProgramId { get; set; }
        /// <summary>
        /// 节目标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Default)]
        public string Title { get; set; }
        /// <summary>
        /// 节目时长
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "duration", Required = Required.Default)]
        public int Duration { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "update_time", Required = Required.Default)]
        public string UpdateTime { get; set; }
        /// <summary>
        /// 节目列表内序号
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "sequence", Required = Required.Default)]
        public int Sequence { get; set; }
        /// <summary>
        /// 是否免费
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "isfree", Required = Required.Default)]
        public string IsFree { get; set; }
        /// <summary>
        /// 节目封面
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "cover", Required = Required.Default)]
        public string Cover { get; set; }
        /// <summary>
        /// 节目类型(未知)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "content_type", Required = Required.Default)]
        public int ContentType { get; set; }
        /// <summary>
        /// 节目播放量
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "playcount", Required = Required.Default)]
        public string PlayCount { get; set; }
    }
}
