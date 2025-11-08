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

                if (int.TryParse(username, out int clientuser))
                {
                    var client = await database.GetClientDataByIdAsync(clientuser);
                    // Add debug information
                    System.Diagnostics.Debug.WriteLine($"Attempting login with ID: {clientuser}");
                    System.Diagnostics.Debug.WriteLine($"Client found: {client != null}");
                    if (client != null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Client password: {client.Password}, Entered password: {password}");
                        // Use string comparison to ensure exact match
                        if (string.Equals(password, client.Password, StringComparison.Ordinal))
                        {
                            await Shell.Current.GoToAsync($"//Client?userId={client.Id}");
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
                System.Diagnostics.Debug.WriteLine($"Login error: {ex}");
            }
            finally
            {
                Password = string.Empty;
            }

        }

    }
}
