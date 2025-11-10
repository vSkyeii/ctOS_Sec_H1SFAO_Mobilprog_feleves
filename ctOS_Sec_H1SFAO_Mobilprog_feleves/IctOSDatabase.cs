using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public interface IctOSDatabase
    {
        Task<List<Client>> GetClientsAsync();
        Task<Client> GetClientDataByIdAsync(int id);
        Task<Device> GetDeviceDataByIdAsync(int id);
        Task<List<Device>> GetClientDevicesByIdAsync(int id);
        Task<List<Device>> GetDevicesAsync();
        Task CreateClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(Client client);
        Task CreateDeviceAsync(Device d);
        Task UpdateDeviceAsync(Device d);
        Task DeleteDeviceAsync(Device d);
    }
}
