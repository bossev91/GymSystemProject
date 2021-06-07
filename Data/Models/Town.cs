using System;
using System.Collections.Generic;
using System.Text;

namespace GymSys.Data.Models
{
    public class Town
    {
        public Town()
        {
            this.Clients = new HashSet<Client>();
            this.Employees = new HashSet<Employee>();
        }

        public int TownId { get; set; }

        public string Name { get; set; }

        public ICollection<Client> Clients { get; set; }

        public ICollection<Employee> Employees { get; set; }


    }
}

// TODO: COUNTRY TABLE 
