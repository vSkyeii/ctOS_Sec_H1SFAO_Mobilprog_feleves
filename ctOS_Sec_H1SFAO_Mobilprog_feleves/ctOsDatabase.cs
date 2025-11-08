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
        string databasePath = Path.Combine(FileSystem.Current.AppDataDirectory, "ctOSSec.db3");
        SQLiteAsyncConnection database;
        public ctOsDatabase()
        {
            database = new SQLiteAsyncConnection(databasePath, Flags);
            database.CreateTableAsync<Client>().Wait();
            database.CreateTableAsync<Device>().Wait();

        }

        public async Task CreateClientAsync(Client client)
        {
            await database.InsertAsync(client);
        }

        public async Task CreateDeviceAsync(Device d)
        {
            await database.InsertAsync(d);
        }

        public async Task DeleteClientAsync(Client client)
        {
            await database.ExecuteAsync("DELETE FROM Device WHERE Id = (SELECT Deviceid FROM Client Where Id= ?)", client.Id);
            await database.ExecuteAsync("DELETE FROM Client WHERE Id = ?", client.Id);
        }

        public async Task DeleteDeviceAsync(Device d)
        {
            await database.DeleteAsync(d);
        }

        public Task<Client> GetClientDataByIdAsync(int id)
        {
            return database.Table<Client>().Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<int>>GetClientDevicesByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Érvénytelen Client ID.", nameof(id));

            // Lekérdezés a Device táblából az adott ClientId alapján
            var devices = await database.Table<Device>().Where(d => d.Id == id).OrderBy(d => d.Name).ToListAsync();
            var deviceIds = devices.Select(d => d.Id).ToList();
            return deviceIds;
        }

        public Task<List<Client>> GetClientsAsync()
        {
            return database.Table<Client>().ToListAsync();
        }

        public Task<List<Device>> GetDevicesAsync()
        {
            return database.Table<Device>().ToListAsync();
        }

        public async Task UpdateClientAsync(Client client)
        {
            await database.UpdateAsync(client);
        }

        public async Task UpdateDeviceAsync(Device d)
        {
            await database.UpdateAsync(d);
        }
    }
}
