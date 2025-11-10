using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using System.Data;
using System.Threading.Tasks;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public partial class DeviceDetailViewModel : ObservableObject
    {
        private readonly IctOSDatabase db;
        private readonly IUserContext userContext;

        private int? currentDeviceId;
        [ObservableProperty] Device sdevice;
        public DeviceDetailViewModel(IctOSDatabase db, IUserContext userContext)
        {
            this.db = db;
            this.userContext = userContext;
        }
        public async Task LoadDeviceAsync()
        {
            currentDeviceId = userContext.SelectedDeviceId;

            if (currentDeviceId is null)
            {
                await Shell.Current.DisplayAlert("Hiba", "Nincs kiválasztott eszköz a UserContext-ben.", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }
            var loadedDevice = await db.GetDeviceDataByIdAsync(currentDeviceId.Value);

            if (loadedDevice != null)
            {
                Sdevice = loadedDevice;
            }
            else
            {
                await Shell.Current.DisplayAlert("Hiba", $"Eszköz (ID: {currentDeviceId.Value}) nem található.", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }

        [ObservableProperty]
        private string connectionStatus = "Ellenőrzésre vár...";

        [RelayCommand]
        private async Task CheckNetworkConnectionAsync()
        {
            string access = Connectivity.NetworkAccess.ToString();
            string profiles = Connectivity.Current.ConnectionProfiles.Aggregate("", (text, profile) => text + profile + ";");
            connectionStatus = access;
            ConnectionStatus = access;
            await Shell.Current.DisplayAlert("Hálózati állapot", $"Hozzáférés: {access}\nProfilok: {profiles}", "OK");
            OnPropertyChanged(nameof(ConnectionStatus));
        }
        [RelayCommand]
        private async Task ToggleDeviceOnlineStatusAsync()
        {
            if (Sdevice == null)
            {
                await Shell.Current.DisplayAlert("Hiba", "Nincs betöltött eszköz az állapot váltásához.", "OK");
                return;
            }
            if (connectionStatus == "Internet")
            {
                Sdevice.IsOnline = !Sdevice.IsOnline;
                try
                {
                    await db.UpdateDeviceAsync(Sdevice);
                    OnPropertyChanged(nameof(Sdevice));
                    await Shell.Current.DisplayAlert("Siker", $"Az eszköz állapota sikeresen frissítve: {(Sdevice.IsOnline ? "Online" : "Offline")}.", "OK");
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Hiba", $"Hiba történt az eszköz állapotának frissítése során: {ex.Message}", "OK");
                }
                
            }
            
        }
    }
}