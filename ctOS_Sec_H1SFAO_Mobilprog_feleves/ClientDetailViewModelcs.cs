using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public partial class ClientDetailViewModel : ObservableObject
    {
        private readonly IctOSDatabase db;
        private readonly IUserContext userContext;

        [ObservableProperty] private int id;
        [ObservableProperty] private string name;
        [ObservableProperty] private string email;
        [ObservableProperty] private string address;

        [ObservableProperty] private bool isEditing;
        [ObservableProperty] private string newPassword;

        public ObservableCollection<Device> Devices { get; } = new ObservableCollection<Device>();
        [ObservableProperty] private Device selectedDevice;
        [ObservableProperty] private bool isAddingDevice;
        [ObservableProperty] private string newDeviceName;
        [ObservableProperty] private DeviceType newDeviceType;
        public IReadOnlyList<DeviceType> DeviceTypes { get; } = Enum.GetValues<DeviceType>().ToList();

        [ObservableProperty] private string displayCooldown = "Shake To Refresh";
        public ClientDetailViewModel(IctOSDatabase db, IUserContext userContext)
        {
            this.db = db;
            this.userContext = userContext;
            IsEditing = false;
            if (Accelerometer.IsSupported)
            {
                Accelerometer.Default.ReadingChanged += Accelerometer_ReadingChanged;
            }
        }
        public async void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading.Acceleration;
            double threshold = 2.0;
            if (Math.Abs(data.X) > threshold || Math.Abs(data.Y) > threshold || Math.Abs(data.Z) > threshold)
            {
                StopAccelerometer();
                await ReloadDevicesAsync();
                await Shell.Current.DisplayAlert("Frissítés", "Eszközlista sikeresen frissítve a telefon mozgásával!", "OK");;
                DisplayCooldown = "Shake Gesture is On Cooldown";
                OnPropertyChanged(nameof(DisplayCooldown));
                await Task.Delay(20000);
                DisplayCooldown = "Shake To Refresh";
                OnPropertyChanged(nameof(DisplayCooldown));
                StartAccelerometer();
            }
        }
        public void StartAccelerometer()
        {
            if (Accelerometer.IsSupported) { Accelerometer.Default.Start(SensorSpeed.UI); }
            
        }
        public void StopAccelerometer()
        {
            if(Accelerometer.IsSupported && Accelerometer.IsMonitoring) { Accelerometer.Default.Stop(); }
            
        }

        public async Task LoadFromContextAsync()
        {
            var clientId = userContext.SelectedClientId;
            if (clientId is null)
            {
                await Shell.Current.DisplayAlert("Error", "No selected client", "OK");
                return;
            }

            var client = await db.GetClientDataByIdAsync(clientId.Value);
            if (client is null)
            {
                await Shell.Current.DisplayAlert("Error", "No such client with this ID", "OK");
                return;
            }
            Id = client.Id;
            Name = client.Name;
            Email = client.Email;
            Address = client.Address;

            await ReloadDevicesAsync();
            IsEditing = false;
        }

        [RelayCommand]
        private void ToggleEdit()
        {
            IsEditing = !IsEditing;
            NewPassword = string.Empty;
            SaveClientDetailsCommand.NotifyCanExecuteChanged();
        }

        private bool CanSaveClientDetails => !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Address);

        [RelayCommand(CanExecute = nameof(CanSaveClientDetails))]
        private async Task SaveClientDetailsAsync()
        {
            var clientToUpdate = await db.GetClientDataByIdAsync(Id);

            if (clientToUpdate == null)
            {
                await Shell.Current.DisplayAlert("Error", "No client to update", "OK");
                return;
            }
            clientToUpdate.Name = Name.Trim();
            clientToUpdate.Email = Email.Trim();
            clientToUpdate.Address = Address.Trim();
            if (!string.IsNullOrWhiteSpace(NewPassword))
            {
                clientToUpdate.Password = NewPassword;
            }

            await db.UpdateClientAsync(clientToUpdate);

            IsEditing = false;
            NewPassword = string.Empty;

            await Shell.Current.DisplayAlert("Siker", "Kliens adatok frissítve.", "OK");
        }
        partial void OnNameChanged(string value) => SaveClientDetailsCommand.NotifyCanExecuteChanged();
        partial void OnEmailChanged(string value) => SaveClientDetailsCommand.NotifyCanExecuteChanged();
        partial void OnAddressChanged(string value) => SaveClientDetailsCommand.NotifyCanExecuteChanged();

        private async Task ReloadDevicesAsync()
        {
            Devices.Clear();
            var list = await db.GetClientDevicesByIdAsync(this.Id);
            foreach (var d in list)
            {
                Devices.Add(d);
            }
        }

        [RelayCommand]
        private async Task DeleteDeviceAsync(Device deviceToDelete)
        {
            if (deviceToDelete == null) return;

            var confirm = await Shell.Current.DisplayAlert(
               "Confirm",
               $"Woukld you like todelete '{deviceToDelete.Name}' Device?",
               "Yes:(",
               "No");
            if (confirm)
            {
                await db.DeleteDeviceAsync(deviceToDelete);
                Devices.Remove(deviceToDelete);
                SelectedDevice = null;
            }
        }

        [RelayCommand]
        private void ToggleAddDevice() => IsAddingDevice = !IsAddingDevice;
        private bool CanSaveNewDevice => !string.IsNullOrWhiteSpace(NewDeviceName);

        [RelayCommand(CanExecute = nameof(CanSaveNewDevice))]
        private async Task SaveNewDeviceAsync()
        {
            var device = new Device
            {
                ClientId = this.Id,
                Name = NewDeviceName?.Trim(),
                Type = NewDeviceType,
                IsOnline = false
            };

            await db.CreateDeviceAsync(device);
            ClearDeviceForm();
            IsAddingDevice = false;
        }

        [RelayCommand]
        private void CancelAddDevice()
        {
            ClearDeviceForm();
            IsAddingDevice = false;
        }

        private void ClearDeviceForm()
        {
            NewDeviceName = string.Empty;
            SaveNewDeviceCommand.NotifyCanExecuteChanged();
        }
        partial void OnNewDeviceNameChanged(string value) => SaveNewDeviceCommand.NotifyCanExecuteChanged();
    }
}