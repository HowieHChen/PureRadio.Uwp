using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Database
{
    /// <summary>
    /// 数据库实体的基础类
    /// </summary>
    public class DbObject
    {
        /// <summary>
        /// 数据库Id
        /// </summary>
        [PrimaryKey]
        [Column(nameof(Id))]
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 项目一级Id
        /// </summary>
        [Column(nameof(MainId))]
        public int MainId { get; set; } = 0;
    }
}
