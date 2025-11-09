using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public partial class AdminViewModel : ObservableObject
    {
        private IctOSDatabase database;
        IUserContext userContext;
        public ObservableCollection<Client> Clients { get; set; }

        [ObservableProperty]
        private Client selectedClient;
        public AdminViewModel(IctOSDatabase database, IUserContext userContext)
        {
            this.database = database;
            this.userContext = userContext;
            Clients = new ObservableCollection<Client>();
        }
        [RelayCommand]
        public async Task LoadClientsAsync()
        {
            var items = await database.GetClientsAsync();
            Clients.Clear();
            foreach (var c in items) Clients.Add(c);
        }

        [RelayCommand]
        private async Task AddNewClientAsync()
        {
            await Shell.Current.GoToAsync("newclient");
            await LoadClientsAsync();

        }
        [RelayCommand]
        private async Task ViewClientDetailsAsync(Client client)
        {
            if (client != null)
            {
                userContext.SelectedClientId = client.Id;
                await Shell.Current.GoToAsync("ClientDetail");
            }
        }
        [RelayCommand]
        private async Task DeleteClientAsync(Client client)
        {
            if (client != null)
            {
                await database.DeleteClientAsync(client);
                Clients.Remove(client);
                if (SelectedClient == client)
                {
                    SelectedClient = null;
                }
                await LoadClientsAsync();
            }
        }
    }
}