using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public partial class AdminViewModel : ObservableObject
    {
        private IctOSDatabase database;
        public ObservableCollection<Client> Clients { get; set; }

        public AdminViewModel(IctOSDatabase database)
        {
            this.database = database;
            Clients = new ObservableCollection<Client>();
        }
    }
}