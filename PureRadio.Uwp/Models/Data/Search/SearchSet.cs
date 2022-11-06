using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Search
{
    /// <summary>
    /// 搜索结果集合.
    /// </summary>
    /// <typeparam name="T">结果类型.</typeparam>
    public class SearchSet<T>
        where T : class
    {
        /// <summary>
        /// 创建 <see cref="SearchSet{T}"/> 的新实例.
        /// </summary>
        /// <param name="items">条目列表.</param>
        /// <param name="isEnd">是否已经获取完全部结果.</param>
        public SearchSet(IEnumerable<T> items, bool isEnd)
        {
            Items = items;
            IsEnd = isEnd;
        }

        /// <summary>
        /// 条目列表.
        /// </summary>
        public IEnumerable<T> Items { get; }

        /// <summary>
        /// 是否已经获取完全部结果.
        /// </summary>
        public bool IsEnd { get; }
    }
}
