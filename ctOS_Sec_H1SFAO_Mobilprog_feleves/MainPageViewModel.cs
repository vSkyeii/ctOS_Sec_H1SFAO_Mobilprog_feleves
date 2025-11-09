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

        public MainPageViewModel(IctOSDatabase database, IUserContext userContext)
        {
            this.database = database;
            Clients = new ObservableCollection<Client>();
            this.userContext = userContext;
        }
        private readonly IUserContext userContext;
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

                if (int.TryParse(username, out int clientuser))
                {
                    var client = await database.GetClientDataByIdAsync(clientuser);
                    if (client != null)
                    {
                        if (password == client.Password)
                        {
                            userContext.Username = clientuser;
                            await Shell.Current.GoToAsync("Client");
                            return;
                        }
                    }
                    ErrorMessage = "Hibás felhasználónév vagy jelszó.";
                }
                else
                {
                    ErrorMessage = "A felhasználónévnek számnak kell lennie.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Bejelentkezési hiba. Próbáld újra.";
            }
            finally
            {
                Password = string.Empty;
                Username = string.Empty;
            }

        }

    }
}
