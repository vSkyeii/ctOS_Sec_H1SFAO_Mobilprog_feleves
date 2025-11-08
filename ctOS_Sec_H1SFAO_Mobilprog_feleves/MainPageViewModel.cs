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
    public partial class MainPageViewModel : ObservableObject
    {
        private IctOSDatabase database;
        public ObservableCollection<Client> Clients { get; set; }

        public MainPageViewModel(IctOSDatabase database)
        {
            this.database = database;
            Clients = new ObservableCollection<Client>();
        }
        [ObservableProperty] private string username = string.Empty;
        [ObservableProperty] private string password = string.Empty;
        [ObservableProperty] private string? errorMessage;

        [RelayCommand]
        private async Task SignInAsync()
        {
            errorMessage = null;
            try
            {
                if (username == "admin" && password == "admin")
                {
                    await Shell.Current.GoToAsync("Admin");
                    return;
                }

                // 2) Client próbálkozás
                int clientuser;
                if (int.TryParse(username, out clientuser))
                {
                    var client = await database.GetClientDataByIdAsync(clientuser);
                    if (client != null && password == client.Password)
                    {
                        await Shell.Current.GoToAsync($"//Client?userId={client.Id}");
                        return;
                    }
                }
                ErrorMessage = "Hibás felhasználónév vagy jelszó.";
            }
            catch (Exception ex)
            {
                ErrorMessage = "Bejelentkezési hiba. Próbáld újra.";
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                Password = string.Empty;
            }

        }

    }
}
