using PureRadio.Uwp.Models.Database;
using PureRadio.Uwp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Args
{
    public class HistoryItemChangedEventArgs : EventArgs
    {
        public HistoryItemChangedEventArgs(
            LibraryItemAction action, 
            History item)
        {
            Action = action;
            Item = item;
        }

        /// <summary>
        /// 历史记录动作类型
        /// </summary>
        public LibraryItemAction Action { get; set; }
        /// <summary>
        /// 影响的项目
        /// </summary>
        public History Item { get; set; }
    }
}
