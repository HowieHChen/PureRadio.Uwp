using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Database
{
    /// <summary>
    /// 代表收藏内容(专辑)
    /// </summary>
    [Table("FavContents")]
    public class FavContent : DbObject, IEquatable<FavContent>
    {
        /// <summary>
        /// 内容(专辑)封面图片(URL)
        /// </summary>
        [Column(nameof(Cover))]
        public string Cover { get; set; }
        /// <summary>
        /// 内容(专辑)标题
        /// </summary>
        [Column(nameof(Title))]
        public string Title { get; set; }
        /// <summary>
        /// 本地内容(专辑)封面图片(URL)
        /// </summary>
        [Column(nameof(LocalCover))]
        public string LocalCover { get; set; }
        /// <summary>
        /// 收藏时间
        /// </summary>
        [Column(nameof(AddTime))]
        public DateTime AddTime { get; set; }

        public FavContent() { }

        public FavContent(
            string cover, int contentId, string title, 
            string localCover, DateTime addTime)
        {
            Cover = cover;
            MainId = contentId;
            Title = title;
            LocalCover = localCover;
            AddTime = addTime;
        }

        /// <summary>
        /// 内容(专辑)标题
        /// </summary>
        public override string ToString()
        {
            return Title;
        }

        public bool Equals(FavContent other)
        {
            return MainId == other.MainId;
        }

        public override int GetHashCode()
        {
            return MainId.GetHashCode();
        }
    }
}
