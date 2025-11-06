using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    internal class ctOsDatabase : IctOSDatabase
    {
        SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create;
        string databasePath =
        Path.Combine(FileSystem.Current.AppDataDirectory, "ctOSSec.db3");
        SQLiteAsyncConnection database;
        public ctOsDatabase()
        {
            database = new SQLiteAsyncConnection(databasePath, Flags);
            database.CreateTableAsync<Client>().Wait();
            database.CreateTableAsync<Admin>().Wait(); 
            database.CreateTableAsync<Device>().Wait();

        }
        public async Task AddClientToAdminAsync(Admin admin)
        {
            throw new NotImplementedException();
        }

        public Task AddDeviceToClientAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public Task CreateAdminAsync(Admin admin)
        {
            throw new NotImplementedException();
        }

        public Task CreateClientAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAdminAsync(Admin admin)
        {
            throw new NotImplementedException();
        }

        public Task DeleteClientAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public Task DeleteClientFromAdminAsync(Admin admin)
        {
            throw new NotImplementedException();
        }

        public Task DeleteDeviceFromClientAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public Task<Admin> GetAdminByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Admin>> GetAdminsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetClientByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetClientByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await database.Table<Client>().ToListAsync();
        }

        public Task UpdateAdminAsync(Admin admin)
        {
            throw new NotImplementedException();
        }

        public Task UpdateClientAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public Task UpdateClientToAdminAsync(Admin admin)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDeviceToClientAsync(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
