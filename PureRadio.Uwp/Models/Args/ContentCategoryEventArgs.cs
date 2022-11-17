using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Args
{
    public sealed class ContentCategoryEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentCategoryEventArgs"/> class.
        /// </summary>
        /// <param name="categoryId">专辑分类Id.</param>
        /// <param name="attributeId">专辑属性Id.</param>
        /// <param name="categoryTitle">专辑分类标题.</param>
        public ContentCategoryEventArgs(
            int categoryId, 
            int attributeId, 
            string categoryTitle)
        {
            CategoryId = categoryId;
            AttributeId = attributeId;
            CategoryTitle = categoryTitle;
        }

        /// <summary>
        /// 专辑分类Id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 专辑属性Id
        /// </summary>
        public int AttributeId { get; set; }
        /// <summary>
        /// 专辑分类标题
        /// </summary>
        public string CategoryTitle { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is ContentCategoryEventArgs args && CategoryId == args.CategoryId && AttributeId == args.AttributeId;

        /// <inheritdoc/>
        public override int GetHashCode()
            => CategoryId.GetHashCode() + AttributeId.GetHashCode();
    }
}
