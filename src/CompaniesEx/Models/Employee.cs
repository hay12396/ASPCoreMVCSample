using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompaniesEx.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public Sex Sex { get; set; }
        public DateTime Birthday { get; set; }

        [ForeignKey("companyId")]
        public int CompanyId { get; set; }
    }
}

public enum Sex
{
    Male,
    Female
}
