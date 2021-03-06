using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiviBank_Core.Models
{
    public class Client
    {
        public Client()
        {
        }

        public Client(DTOs.dtoClient dtoClient)
        {
            Id = dtoClient.id;
            Name = dtoClient.name;
            BirthDate = Convert.ToDateTime(dtoClient.birthDate);
            Contact = dtoClient.contact;
        }

        public int? Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Contact { get; set; }

        public ICollection<Loan> Loans { get; set; }


    }
}
