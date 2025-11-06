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
        private int userid;
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
        public async Task SetUserIdAsync(int id)
        {
            userid = id;
            await InitializeAsync();
        }
        public ClientViewModel(IctOSDatabase database)
        {
            this.database = database;
            Devices = new ObservableCollection<Device>();
            //Devices.Add();
            //Devices.Add();
        }
        public async Task InitializeAsync()
        {
            var client = await database.GetClientByIdAsync(userid);
            Devices.Clear();
            client.Devices.ForEach(p => Devices.Add(p));
        }
    }
}
