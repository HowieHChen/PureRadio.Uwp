using PureRadio.Uwp.Models.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PureRadio.Uwp.Models.Data.Constants.ApiConstants;

namespace PureRadio.Uwp.Models.Database
{
    /// <summary>
    /// 代表历史播放
    /// </summary>
    [Table("Histories")]
    public class History : DbObject, IEquatable<History>
    {
        /// <summary>
        /// 项目类型
        /// </summary>
        [Column(nameof(Type))]
        public MediaPlayType Type { get; set; }
        /// <summary>
        /// 封面图片(URL)
        /// </summary>
        [Column(nameof(Cover))]
        public string Cover { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Column(nameof(Title))]
        public string Title { get; set; }
        /// <summary>
        /// 副标题
        /// </summary>
        [Column(nameof(SubTitle))]
        public string SubTitle { get; set; }
        /// <summary>
        /// 二级Id
        /// </summary>
        [Column(nameof(SecondaryId))]
        [NotNull]
        public int SecondaryId { get; set; }
        /// <summary>
        /// 本地封面图片(URL)
        /// </summary>
        [Column(nameof(LocalCover))]
        public string LocalCover { get; set; }
        /// <summary>
        /// 上次播放时间
        /// </summary>
        [Column(nameof(LastPlayTime))]
        public DateTime LastPlayTime { get; set; }

        public History() { }

        public History(
            MediaPlayType type, string cover, string title, 
            string subTitle, int mainId, int secondaryId, 
            string localCover, DateTime lastPlayTime)
        {
            Type = type;
            Cover = cover;
            Title = title;
            SubTitle = subTitle;
            MainId = mainId;
            SecondaryId = secondaryId;
            LocalCover = localCover;
            LastPlayTime = lastPlayTime;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public override string ToString()
        {
            return Title;
        }

        public bool Equals(History other)
        {
            return MainId == other.MainId && SecondaryId == other.SecondaryId;
        }

        public override int GetHashCode()
        {
            return MainId.GetHashCode() + SecondaryId.GetHashCode();
        }
    }
}
