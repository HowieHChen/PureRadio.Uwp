using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Radio
{
    /// <summary>
    /// 电台推荐响应
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class RadioRecommendResponse
    {
        /// <summary>
        /// 电台推荐响应数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "data", Required = Required.Default)]
        public RadioRecommendResponseData Data { get; set; }
    }

    public class RadioRecommendResponseData
    {
        /// <summary>
        /// 电台推荐数据
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "homePage", Required = Required.Default)]
        public RadioHomePage Radios { get; set; }
    }

    public class RadioHomePage
    {
        /// <summary>
        /// 电台推荐列表
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "radioData", Required = Required.Default)]
        public List<RadioRecommendItem> RadioItems { get; set; }
    }

    


}
