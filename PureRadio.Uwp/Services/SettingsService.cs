using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.Xaml;

#nullable enable

namespace PureRadio.Uwp.Services
{
    public sealed class SettingsService : ISettingsService
    {
        /// <summary>
        /// The <see cref="IPropertySet"/> with the settings targeted by the current instance.
        /// </summary>
        private readonly IPropertySet SettingsStorage = ApplicationData.Current.LocalSettings.Values;

        public event EventHandler<ElementTheme>? SettingTheme;

        public ThreadPoolTimer? SleepTimer { get; set; }
        public bool TimerStatus { get; set; } = false;
        public string ShutdownTimeString { get; set; } = string.Empty;

        /// <inheritdoc/>
        public void SetValue<T>(string key, T? value)
        {
            if (!SettingsStorage.ContainsKey(key)) SettingsStorage.Add(key, value);
            else SettingsStorage[key] = value;
        }

        /// <inheritdoc/>
        public T? GetValue<T>(string key)
        {
            if (SettingsStorage.TryGetValue(key, out object? value))
            {
                return (T)value!;
            }

            return default;
        }

        public void SetTheme(ElementTheme theme)
        {
            SettingTheme?.Invoke(this, theme);
        }

        public void SetTimer(TimeSpan delay)
        {
            if (!TimerStatus)
            {
                SleepTimer = ThreadPoolTimer.CreateTimer(
                (timer) =>
                {
                    CoreApplication.Exit();
                },
                delay);
                ShutdownTimeString = DateTime.Now.AddMilliseconds(delay.TotalMilliseconds).ToString("g");
                TimerStatus = true;
                var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
                ShutdownTimeString = resourceLoader.GetString("PageSettingsTimerDelay") + ShutdownTimeString;
            }
        }

        public void CancelTimer()
        {
            if (TimerStatus)
            {
                SleepTimer?.Cancel();
                TimerStatus = false;
                ShutdownTimeString = string.Empty;
            }
        }
    }
}
