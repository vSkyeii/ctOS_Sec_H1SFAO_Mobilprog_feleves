using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public partial class AdminViewModel : ObservableObject
    {
        private IctOSDatabase database;
        public ObservableCollection<Client> Clients { get; set; }

        [ObservableProperty]
        private Client? selectedClient;

        public AdminViewModel(IctOSDatabase database)
        {
            this.database = database;
            Clients = new ObservableCollection<Client>();
            Clients.Add(new Client { Address = "sigma street", Deviceid = "12", Email = "sigmamail@hh.com", Name = "sigma poeter", Password = "alma" });
        }
        [RelayCommand]
        private async Task LoadClientsAsync()
        {
            var items = await database.GetClientsAsync();
            Clients.Clear();
            foreach (var c in items) Clients.Add(c);
        }

        [RelayCommand]
        private Task AddNewClientAsync()
        {
            return Shell.Current.GoToAsync("newclient");
        }
    }
}