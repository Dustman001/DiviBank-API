using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiviBank_Core.Models;
using DiviBank_Core.Models.Db;
using DiviBank_Core.Services;
using DiviBank_Core.Models.DTOs;
using System.Text.Json;

namespace DiviBank_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly clientService _clientService;

        public ClientsController(clientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<JsonResult> GetClient()
        {
            try
            {
                return new JsonResult(await _clientService.getListAsync());
            }
            catch (Exception x)
            {
                throw;
            }

        }
        
        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetClient(int id)
        {
            var dtoclient = await _clientService.FindAsync(id);

            if (dtoclient == null)
            {
                return NotFound();
            }

            return  JsonSerializer.Serialize(dtoclient);
        }

        // GET: api/Clients
        [HttpGet("Loans")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientWithLoans()
        {
            try
            {
                return await _clientService.getLoanListAsync();
            }
            catch (Exception x)
            {
                throw;
            }
        }
        
        // GET: api/Clients/5
        [HttpGet("Loans/{id}")]
        public async Task<ActionResult<Client>> GetClientLoan(int id)
        {
            var client = await _clientService.FindLoanAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, dtoClient client)
        {
            if (id != client.id)
            {
                return BadRequest();
            }

            try
            {
                await _clientService.Update(client);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<dtoClient>> PostClient([FromBody]dtoClient client)
        {
            try
            {
                //dtoClient client = JsonSerializer.Deserialize<dtoClient>(data.Replace("'","\""));

                try
                {
                    client = await _clientService.Save(client);
                }catch(Exception x)
                {
                    throw;
                }

                return CreatedAtAction("GetClient", new { id = client.id }, client);
            }
            catch (Exception x)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _clientService.Delete(id);
            
            if (client == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _clientService.ifany(id);
        }
    }
}
