using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Local
{
    /// <summary>
    /// 电台分类项(本地)
    /// </summary>
    public class RadioCategoryItem
    {
        public RadioCategoryItem(int categoryId, string cover)
        {
            CategoryId = categoryId;
            Cover = cover;
        }

        /// <summary>
        /// 分类Id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 封面URL
        /// </summary>
        public string Cover { get; set; }

    }
}
