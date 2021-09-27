using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiviBank_Core.Models.DTOs
{
    public class dtoClient
    {
        public dtoClient(Client client)
        {
            id = client.Id;
            name = client.Name;
            birthDate = client.BirthDate.ToString("yyyy/MM/dd");
            contact = client.Contact;
        }

        public dtoClient()
        { }

        public int? id { get; set; }

        public string name { get; set; }

        public string birthDate { get; set; }

        public string contact { get; set; }


    }
}
