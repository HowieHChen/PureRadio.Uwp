using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Content
{
    public class ContentPlaylistDetail
    {
        public ContentPlaylistDetail
            (string version, int programId, string title, int duration, string updateTime, 
            int sequence, bool isFree, Uri cover, int contentType, string playCount)
        {
            Version = version;
            ProgramId = programId;
            Title = title;
            Duration = duration;
            UpdateTime = updateTime;
            Sequence = sequence;
            IsFree = isFree;
            Cover = cover;
            ContentType = contentType;
            PlayCount = playCount;
        }


        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 节目Id
        /// </summary>
        public int ProgramId { get; set; }
        /// <summary>
        /// 节目标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 节目时长
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime { get; set; }
        /// <summary>
        /// 节目列表内序号
        /// </summary>
        public int Sequence { get; set; }
        /// <summary>
        /// 是否免费
        /// </summary>
        public bool IsFree { get; set; }
        /// <summary>
        /// 节目封面
        /// </summary>
        public Uri Cover { get; set; }
        /// <summary>
        /// 节目类型(未知)
        /// </summary>
        public int ContentType { get; set; }
        /// <summary>
        /// 节目播放量
        /// </summary>
        public string PlayCount { get; set; }
    }
}
