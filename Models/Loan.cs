using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiviBank_Core.Models
{
    public class Loan
    {
        public Loan()
        {

        }

        public Loan(DTOs.dtoLoan dtoLoan)
        {
            Id = dtoLoan.Id;
            Date = Convert.ToDateTime(dtoLoan.Date);
            Amount = dtoLoan.Amount;
            ClientId = dtoLoan.ClientId;
        }

        public int? Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public int ClientId { get; set; }
    }
}