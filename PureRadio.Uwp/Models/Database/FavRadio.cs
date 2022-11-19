using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Database
{
    /// <summary>
    /// 代表收藏电台
    /// </summary>
    [Table("FavRadios")]
    public class FavRadio : DbObject, IEquatable<FavRadio>
    {
        /// <summary>
        /// 电台标题
        /// </summary>
        [Column(nameof(Title))]
        public string Title { get; set; }
        /// <summary>
        /// 电台封面图片(URL)
        /// </summary>
        [Column(nameof(Cover))]
        public string Cover { get; set; }
        /// <summary>
        /// 本地电台封面图片(URL)
        /// </summary>
        [Column(nameof(LocalCover))]
        public string LocalCover { get; set; }
        /// <summary>
        /// 收藏时间
        /// </summary>
        [Column(nameof(AddTime))]
        public DateTime AddTime { get; set; }

        public FavRadio() { }

        public FavRadio(
            int radioId, string title, string cover, 
            string localCover, DateTime addTime)
        {
            MainId = radioId;
            Title = title;
            Cover = cover;
            LocalCover = localCover;
            AddTime = addTime;
        }

        /// <summary>
        /// 返回电台标题
        /// </summary>
        public override string ToString()
        {
            return Title;
        }

        public bool Equals(FavRadio other)
        {
            return MainId == other.MainId;
        }

        public override int GetHashCode()
        {
            return MainId.GetHashCode();
        }
    }
}
