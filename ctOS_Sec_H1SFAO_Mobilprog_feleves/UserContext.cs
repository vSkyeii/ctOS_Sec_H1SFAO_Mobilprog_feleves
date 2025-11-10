using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public sealed class UserContext : IUserContext
    {
        public int Username { get; set; }
        public int? SelectedClientId { get; set; }
        public int? SelectedDeviceId { get; set; }
    }
}
