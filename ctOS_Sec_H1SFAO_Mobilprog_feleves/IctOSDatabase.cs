using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public interface IctOSDatabase
    {
        Task<List<Admin>> GetAdminsAsync();
        Task<List<Client>> GetClientsAsync();
        Task<Admin> GetAdminByIdAsync(int id);
        Task<Client> GetClientByIdAsync(int id);
        Task CreateClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(Client client);
        Task AddDeviceToClientAsync(Client client);
        Task UpdateDeviceToClientAsync(Client client);
        Task DeleteDeviceFromClientAsync(Client client);
        Task AddClientToAdminAsync(Admin admin);
        Task UpdateClientToAdminAsync(Admin admin);
        Task DeleteClientFromAdminAsync(Admin admin);
        Task CreateAdminAsync(Admin admin);
        Task UpdateAdminAsync(Admin admin);
        Task DeleteAdminAsync(Admin admin);
    }
}
