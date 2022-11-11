using CommunityToolkit.Mvvm.ComponentModel;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class RadioDetailViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly IRadioProvider radioProvider;
        private readonly DispatcherTimer _refreshTimer;

        private int _radioId;
        public int RadioId
        {
            get => _radioId;
            set
            {
                SetProperty(ref _radioId, value);
                GetRadioDetail();
                GetRadioPlaylist();
            }
        }

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private List<RadioPlaylistDetail> _playlistsBYDay;
        [ObservableProperty]
        private List<RadioPlaylistDetail> _playlistsYDay;
        [ObservableProperty]
        private List<RadioPlaylistDetail> _playlistsToday;
        [ObservableProperty]
        private List<RadioPlaylistDetail> _playlistsTMR;

        public RadioDetailViewModel(
            INavigateService navigate, 
            IRadioProvider radioProvider)
        {
            this.navigate = navigate;
            this.radioProvider = radioProvider;
            IsActive = true;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }

        protected override void OnDeactivated()
        {

            base.OnDeactivated();
        }

        private async void GetRadioDetail()
        {
            if(RadioId != 0)
            {
                var result = await radioProvider.GetRadioDetailInfo(RadioId, CancellationToken.None);

            }
        }

        private async void GetRadioPlaylist()
        {
            if(RadioId != 0)
            {
                var result = await radioProvider.GetRadioPlaylistDetail(RadioId, CancellationToken.None);
                if(result != null)
                {
                    PlaylistsBYDay = result?.BYday ?? new List<RadioPlaylistDetail>();
                    PlaylistsYDay = result?.Yday ?? new List<RadioPlaylistDetail>();
                    PlaylistsToday = result?.Today ?? new List<RadioPlaylistDetail>();
                    PlaylistsTMR = result?.Tmr ?? new List<RadioPlaylistDetail>();
                }
                else
                {
                    PlaylistsBYDay = new List<RadioPlaylistDetail>();
                    PlaylistsYDay = new List<RadioPlaylistDetail>();
                    PlaylistsToday = new List<RadioPlaylistDetail>();
                    PlaylistsTMR = new List<RadioPlaylistDetail>();
                }
            }
        }

    }
}
