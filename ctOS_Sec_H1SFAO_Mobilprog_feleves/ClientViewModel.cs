using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public partial class ClientViewModel : ObservableObject
    {
        private IctOSDatabase database;
        private readonly IUserContext userContext;
        public ObservableCollection<Device> Devices { get; set; }

        [ObservableProperty]
        Client selectedDevice;
        public bool IsFullViewEnabled
        {
            get
            {
                return Preferences.Default.Get("fullview", true);
            }
            set
            {
                Preferences.Default.Set("fullview", value);
                OnPropertyChanged();
            }

        }
        public ClientViewModel(IctOSDatabase database, IUserContext userContext)
        {
            this.database = database;
            Devices = new ObservableCollection<Device>();
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
    }
}
