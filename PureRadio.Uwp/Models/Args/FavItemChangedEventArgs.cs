using PureRadio.Uwp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Args
{
    public class FavItemChangedEventArgs : EventArgs
    {
        public FavItemChangedEventArgs(
            LibraryItemAction action, 
            MediaPlayType itemType, 
            int mainId,
            object parameter)
        {
            Action = action;
            ItemType = itemType;
            MainId = mainId;
            Parameter = parameter;
        }

        /// <summary>
        /// 收藏动作类型
        /// </summary>
        public LibraryItemAction Action { get; set; }
        /// <summary>
        /// 收藏项目类型
        /// </summary>
        public MediaPlayType ItemType { get; set; }
        /// <summary>
        /// 收藏项目一级Id
        /// </summary>
        public int MainId { get; set; }
        /// <summary>
        /// 附加参数
        /// </summary>
        public object Parameter { get; }
    }
}
