using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public partial class Admin:ObservableObject
    {
        [ObservableProperty]
        int id;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string clientid;
    }
}
