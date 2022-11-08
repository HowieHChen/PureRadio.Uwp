using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Radio
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RadioDetailItem
    {
        /// <summary>
        /// 电台ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id", Required = Required.Default)]
        public int RadioId { get; set; }
        /// <summary>
        /// 电台标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Default)]
        public string Title { get; set; }
        /// <summary>
        /// 电台封面图片(URL)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "cover", Required = Required.Default)]
        public string Cover { get; set; }
        /// <summary>
        /// 电台简介
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Required.Default)]
        public string Description { get; set; }
        /// <summary>
        /// 正在播放的节目
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "nowplaying", Required = Required.Default)]
        public NowplayingProgram Nowplaying { get; set; }
        /// <summary>
        /// 电台听众计数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "audience_count", Required = Required.Default)]
        public int AudienceCount { get; set; }
        /// <summary>
        /// 所属分类Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "top_category_id", Required = Required.Default)]
        public int TopCategoryId { get; set; }
        /// <summary>
        /// 所属分类标题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "top_category_title", Required = Required.Default)]
        public string TopCategoryTitle { get; set; }
        /// <summary>
        /// 所属地区Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "region_id", Required = Required.Default)]
        public int RegionId { get; set; }
        /// <summary>
        /// 所属城市Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "city_id", Required = Required.Default)]
        public int CityId { get; set; }
    }


    [JsonObject(MemberSerialization.OptIn)]
    public class NowplayingProgram
    {
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
    }
}
