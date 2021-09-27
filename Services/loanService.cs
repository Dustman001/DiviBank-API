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
    public class loanService
    {
        DiviContext _divicontext;

        public loanService(DiviContext divic)
        {
            this._divicontext = divic;
        }

        public async Task<List<dtoLoan>> getListAsync()
        {
            List<dtoLoan> dtoLoans = new List<dtoLoan>();

            var loans = await _divicontext.Loans.ToListAsync();

            loans.ForEach(fe =>
            {
                dtoLoans.Add(new dtoLoan(fe, _divicontext));
            });

            return dtoLoans;
        }

        public async Task<List<Loan>> getLoanListAsync() =>
            await _divicontext.Loans.ToListAsync();

        internal async Task<dtoLoan> FindAsync(int id)
        {
            var loan = await _divicontext.Loans.FindAsync(id);

            if (loan == null)
                return null;
            else
                return new dtoLoan(loan, _divicontext);
        }

        internal async Task<Loan> FindLoanAsync(int id)
        {
            var loan = await _divicontext.Loans.Where(w => w.Id == id).FirstOrDefaultAsync();

            if (loan == null)
                return null;
            else
                return loan;
        }

        internal async Task<int> Update(dtoLoan dtoLoan)
        {
            Loan loan = new Loan(dtoLoan);

            _divicontext.Entry(loan).State = EntityState.Modified;

            return await _divicontext.SaveChangesAsync();
        }

        internal async Task<dtoLoan> Save(dtoLoan dtoLoan)
        {
            var loan = new Loan(dtoLoan);

            _divicontext.Loans.Add(loan);

            await _divicontext.SaveChangesAsync();

            return new dtoLoan(loan, _divicontext);
        }

        internal bool ifany(int id)
        {
            return _divicontext.Loans.Any(e => e.Id == id);
        }

        internal async Task<int?> Delete(int id)
        {
            var loan = await _divicontext.Loans.FindAsync(id);

            if (loan == null)
            {
                return null;
            }
            else
            {
                _divicontext.Loans.Remove(loan);
                return await _divicontext.SaveChangesAsync();
            }
        }
    }
}
