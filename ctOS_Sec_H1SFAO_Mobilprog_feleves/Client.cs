using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public partial class Client : ObservableObject
    {
        [ObservableProperty]
        [property:PrimaryKey]
        [property:AutoIncrement]
        int id;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string address;
        [ObservableProperty]
        string? deviceid;
        [ObservableProperty]
        string? password;
    }
}
