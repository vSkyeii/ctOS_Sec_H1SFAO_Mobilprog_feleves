using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public partial class ClientViewModel : ObservableObject
    {
        private IctOSDatabase database;
        private readonly IUserContext userContext;
        public ObservableCollection<Device> Devices { get; } = new ObservableCollection<Device>();

        [ObservableProperty]
        private Device selectedDevice;
        public bool IsFullViewEnabled
        {
            get => Preferences.Default.Get("fullview", true);
            set
            {
                Preferences.Default.Set("fullview", value);
                OnPropertyChanged();
            }
        }

        public ClientViewModel(IctOSDatabase database, IUserContext userContext)
        {
            this.database = database;
            this.userContext = userContext;
        }
        public async Task InitializeAsync()
        {
            Devices.Clear();
            var client_devices = await database.GetClientDevicesByIdAsync(userContext.Username);
            foreach (var d in client_devices)
            {
                Devices.Add(d);
            }
        }

        [RelayCommand]
        private async Task ViewDeviceDetailsAsync(Device device)
        {
            if (device == null)
            {
                await Shell.Current.DisplayAlert("Hiba", "Válasszon eszközt a részletekhez.", "OK");
                return;
            }
            userContext.SelectedDeviceId = device.Id;

            await Shell.Current.GoToAsync("DeviceDetail");
            SelectedDevice = null;
        }
    }
}