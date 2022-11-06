using PureRadio.Uwp.Models.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Xaml;

namespace PureRadio.Uwp.Services.Interfaces
{
    public interface ISettingsService
    {
        /// <summary>
        /// Assigns a value to a settings key.
        /// </summary>
        /// <typeparam name="T">The type of the object bound to the key.</typeparam>
        /// <param name="key">The key to check.</param>
        /// <param name="value">The value to assign to the setting key.</param>
        void SetValue<T>(string key, T value);

        /// <summary>
        /// Reads a value from the current <see cref="IServiceProvider"/> instance and returns its casting in the right type.
        /// </summary>
        /// <typeparam name="T">The type of the object to retrieve.</typeparam>
        /// <param name="key">The key associated to the requested object.</param>
        T GetValue<T>(string key);

        /// <summary>
        /// 修改主题事件
        /// </summary>
        event EventHandler<ElementTheme> SettingTheme;

        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="theme"></param>
        void SetTheme(ElementTheme theme);

        /// <summary>
        /// 睡眠定时器
        /// </summary>
        ThreadPoolTimer SleepTimer { get; }

        /// <summary>
        /// 定时器状态
        /// </summary>
        bool TimerStatus { get; }

        /// <summary>
        /// 定时器触发时间（应用推退出时间）
        /// </summary>
        string ShutdownTimeString { get; }

        /// <summary>
        /// 设置定时器
        /// </summary>
        /// <param name="delay"></param>
        void SetTimer(TimeSpan delay);

        /// <summary>
        /// 取消定时器
        /// </summary>
        void CancelTimer();
    }
}
