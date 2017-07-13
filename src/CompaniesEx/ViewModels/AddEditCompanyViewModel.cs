using CompaniesEx.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompaniesEx.ViewModels
{
    public class AddEditCompanyViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        public int PHC { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public AddEditCompanyViewModel()
        {
            Employees = new List<Employee>();
        }
    }
}
