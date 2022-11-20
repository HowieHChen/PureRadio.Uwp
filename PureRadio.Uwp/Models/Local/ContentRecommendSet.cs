using Newtonsoft.Json;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.QingTing.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Local
{
    /// <summary>
    /// 内容分类推荐集合
    /// </summary>
    public class ContentRecommendSet
    {
        public ContentRecommendSet(
            int categoryId, string categoryTittle, 
            List<ContentInfoRecommend> categoryItems)
        {
            CategoryId = categoryId;
            CategoryTittle = categoryTittle;
            CategoryItems = categoryItems;
        }


        /// <summary>
        /// 分类Id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 分类标题
        /// </summary>
        public string CategoryTittle { get; set; }
        /// <summary>
        /// 所属的推荐内容
        /// </summary>
        public List<ContentInfoRecommend> CategoryItems { get; set; }
    }
}
