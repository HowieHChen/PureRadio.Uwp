using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Radio
{
    /// <summary>
    /// 电台播放列表响应
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class RadioPlaylistResponse
    {
        /// <summary>
        /// 电台播放列表成功标志
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Success", Required = Required.Default)]
        public string Success { get; set; }
        /// <summary>
        /// 电台播放列表响应数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Data", Required = Required.Default)]
        public PlaylistArray Data { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class PlaylistArray
    {
        /// <summary>
        /// 周日节目单 (Day 1)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "1", Required = Required.Default)]
        public List<RadioPlaylistItem> Sunday { get; set; }
        /// <summary>
        /// 周一节目单 (Day 2)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "2", Required = Required.Default)]
        public List<RadioPlaylistItem> Monday { get; set; }
        /// <summary>
        /// 周二节目单 (Day 3)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "3", Required = Required.Default)]
        public List<RadioPlaylistItem> Tuesday { get; set; }
        /// <summary>
        /// 周三节目单 (Day 4)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "4", Required = Required.Default)]
        public List<RadioPlaylistItem> Wednesday { get; set; }
        /// <summary>
        /// 周四节目单 (Day 5)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "5", Required = Required.Default)]
        public List<RadioPlaylistItem> Thursday { get; set; }
        /// <summary>
        /// 周五节目单 (Day 6)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "6", Required = Required.Default)]
        public List<RadioPlaylistItem> Friday { get; set; }
        /// <summary>
        /// 周六节目单 (Day 7)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "7", Required = Required.Default)]
        public List<RadioPlaylistItem> Saturday { get; set; }
    }

}
