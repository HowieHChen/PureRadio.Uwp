using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class SettingsViewModel : ObservableRecipient
    {
        private readonly ISettingsService settings;// = Ioc.Default.GetRequiredService<ISettingsService>();
        // 展示重启窗体事件
        public event EventHandler ShowRestartDialog;

        private string savedLanguage;

        private ElementTheme _theme;
        public ElementTheme Theme
        {
            get => _theme;
            set
            {
                SetProperty(ref _theme, value);
                SetTheme();
            }

        }
        private string _language;
        public string Language
        {
            get => _language;
            set
            {
                if (savedLanguage != value && savedLanguage == _language) ShowRestartDialog?.Invoke(this, new EventArgs());
                SetProperty(ref _language, value);
            }
        }
        private bool _timerStatus;
        public bool TimerStatus
        {
            get => _timerStatus;
            set
            {
                if (value != _timerStatus)
                {
                    if (_timerStatus) settings.CancelTimer();
                    else if (!_timerStatus && Delay is not null) settings.SetTimer((TimeSpan)Delay);
                    SetProperty(ref _timerStatus, value);
                    CloseTime = settings.ShutdownTimeString;
                    Delay = null;
                }
            }
        }
        private TimeSpan? _delay;
        public TimeSpan? Delay
        {
            get => _delay;
            set
            {
                SetProperty(ref _delay, value);
                TimerToggleEnabled = (value is not null || TimerStatus);
            }
        }
        private string _closeTime;
        public string CloseTime
        {
            get => _closeTime;
            set
            {
                SetProperty(ref _closeTime, value);
            }
        }
        private bool _timerToggleEnabled;
        public bool TimerToggleEnabled
        {
            get => _timerToggleEnabled;
            set
            {
                SetProperty(ref _timerToggleEnabled, value);
            }
        }
        [ObservableProperty]
        private bool _clearingCache;

        public SettingsViewModel(ISettingsService settings)
        {
            this.settings = settings;
            IsActive = true;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            _theme = App.RootTheme;
            savedLanguage = _language = settings.GetValue<string>(AppConstants.SettingsKey.ConfigLanguage) ?? AppConstants.SettingsValue.Auto;
            _timerStatus = settings.TimerStatus;
            _closeTime = settings.ShutdownTimeString;
            Delay = null;
        }


        private void SetTheme()
        {
            settings.SetTheme(Theme);
            string value = Theme switch
            {
                ElementTheme.Light => AppConstants.SettingsValue.Light,
                ElementTheme.Dark => AppConstants.SettingsValue.Dark,
                _ => AppConstants.SettingsValue.Default
            };
            settings.SetValue(AppConstants.SettingsKey.ConfigTheme, value);
        }

        public async Task RestartAsync()
        {
            settings.SetValue(AppConstants.SettingsKey.ConfigLanguage, Language);
            await CoreApplication.RequestRestartAsync(string.Empty);
        }

        public void ResetLanguageSelected()
        {
            Language = savedLanguage;
        }

        public void ClearCache()
        {
            ClearingCache = true;
        }
    }
}
