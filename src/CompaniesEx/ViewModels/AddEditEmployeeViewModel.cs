using System;
using System.ComponentModel.DataAnnotations;

namespace CompaniesEx.ViewModels
{
    public class AddEditEmployeeViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [IdNumberAttribute("Please enter a valid Id Number.")]
        public string IdNumber { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public int CompanyId { get; set; }
    }
}
