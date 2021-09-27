using DiviBank_Core.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiviBank_Core.Models.DTOs
{
    public class dtoLoan
    {
        public dtoLoan()
        {
        }

        public dtoLoan(Loan loan, DiviContext _diviContext)
        {
            var clientName = _diviContext.Clients.Where(w => w.Id == loan.ClientId).FirstOrDefault().Name;
            Id = loan.Id;
            Date = loan.Date.ToString("yyyy/MM/dd");
            Amount = loan.Amount;
            ClientId = loan.ClientId;
            ClientName = clientName;
        }

        public int? Id { get; set; }

        public string Date { get; set; }

        public decimal Amount { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; }

    }
}
