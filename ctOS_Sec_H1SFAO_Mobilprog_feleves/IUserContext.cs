using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public interface IUserContext
    {
        int Username { get; set; }
        int? SelectedClientId { get; set; }
        int? SelectedDeviceId { get; set; }
    }
}
