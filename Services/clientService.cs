using DiviBank_Core.Models;
using DiviBank_Core.Models.Db;
using DiviBank_Core.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiviBank_Core.Services
{
    public class clientService
    {
        private readonly DiviContext _divicontext;

        public clientService(DiviContext divic)
        {
            this._divicontext = divic;
        }

        public async Task<List<dtoClient>> getListAsync()
        {
            List<dtoClient> dtoClients = new List<dtoClient>();

            var clients = await _divicontext.Clients.ToListAsync();
            clients.ForEach(fe =>
            {
                dtoClients.Add(new dtoClient(fe));
            });

            return dtoClients;
        }
            
        public async Task<List<Client>> getLoanListAsync() => 
            await _divicontext.Clients.Include(i => i.Loans).ToListAsync();

        internal async Task<dtoClient> FindAsync(int id)
        {
            var client = await _divicontext.Clients.FindAsync(id);

            if (client == null)
                return null;
            else
                return new dtoClient(client);
        }

        internal async Task<Client> FindLoanAsync(int id)
        {
            var client = await _divicontext.Clients.Include(i => i.Loans).Where(w => w.Id == id).FirstOrDefaultAsync();

            if (client == null)
                return null;
            else
                return client;
        }

        internal async Task<int> Update(dtoClient dtoclient)
        {
            Client client = new Client(dtoclient);

            _divicontext.Entry(client).State = EntityState.Modified;

            return await _divicontext.SaveChangesAsync();
        }

        internal async Task<dtoClient> Save(dtoClient dtoclient)
        {
            var client = new Client(dtoclient);

            _divicontext.Clients.Add(client);

            await _divicontext.SaveChangesAsync();

            return new dtoClient(client);
        }

        internal bool ifany(int id)
        {
            return _divicontext.Clients.Any(e => e.Id == id);
        }

        internal async Task<int?> Delete(int id)
        {
            var client = await _divicontext.Clients.FindAsync(id);

            if (client == null)
            {
                return null;
            }
            else
            {
                _divicontext.Clients.Remove(client);
                return await _divicontext.SaveChangesAsync();
            }
        }
    }
}
