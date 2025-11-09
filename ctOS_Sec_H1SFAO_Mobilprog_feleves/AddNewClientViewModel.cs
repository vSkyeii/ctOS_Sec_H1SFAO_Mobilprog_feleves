using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public partial class AddNewClientViewModel : ObservableObject
    {
        private readonly IctOSDatabase db;

        [ObservableProperty] private string name;
        [ObservableProperty] private string email;
        [ObservableProperty] private string deviceid;
        [ObservableProperty] private string password;
        [ObservableProperty] private string address;

        public AddNewClientViewModel(IctOSDatabase db)
        {
            this.db = db;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            await db.CreateClientAsync(new Client
            {
                Name = Name,
                Email = Email,
                Deviceid = "",
                Password = Password,
                Address = Address

            });
            await Shell.Current.GoToAsync("..");
        }
    }
}
