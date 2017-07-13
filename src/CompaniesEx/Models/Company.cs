using System.Collections.Generic;

namespace CompaniesEx.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int PHC { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public Company()
        {
            Employees = new List<Employee>();
        }
    }
}
