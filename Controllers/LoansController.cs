using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiviBank_Core.Models;
using DiviBank_Core.Models.Db;
using DiviBank_Core.Models.DTOs;
using DiviBank_Core.Services;
using System.Text.Json;

namespace DiviBank_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly loanService _loanService;

        public LoansController(loanService loanService)
        {
            this._loanService = loanService;
        }

        // GET: api/Loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dtoLoan>>> GetLoanList()
        {
            try
            {
                return new JsonResult(await _loanService.getListAsync());
            }catch(Exception ex)
            {
                throw;
            }
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetLoan(int id)
        {
            var dtoloan = await _loanService.FindAsync(id);

            if (dtoloan == null)
            {
                return NotFound();
            }

            return JsonSerializer.Serialize(dtoloan);
        }

        // PUT: api/Loans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoan(int id, dtoLoan loan)
        {
            if (id != loan.Id)
            {
                return BadRequest();
            }

            try
            {
                await _loanService.Update(loan);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(id))
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

        // POST: api/Loans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(dtoLoan loan)
        {
            try
            {
                //dtoClient client = JsonSerializer.Deserialize<dtoClient>(data.Replace("'","\""));

                try
                {
                    loan = await _loanService.Save(loan);
                }
                catch (Exception x)
                {
                    throw;
                }

                return CreatedAtAction("GetClient", new { id = loan.Id }, loan);
            }
            catch (Exception x)
            {
                return BadRequest();
            }
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var client = await _loanService.Delete(id);

            if (client == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool LoanExists(int id)
        {
            return _loanService.ifany(id);
        }
    }
}
