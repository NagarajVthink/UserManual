using System.ComponentModel.DataAnnotations;

namespace EmployeManagement.Models.Employees
{
    public abstract class EmployeBase
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postal { get; set; }
        public string Country { get; set; }
    }

    public class CreateEmploye : EmployeBase
    {
        [Required(ErrorMessage = "CreatedBy is required")]
        public string CreatedBy { get; set; }
    }

    public class UpdateEmploye : EmployeBase
    {
        [Required(ErrorMessage = "EmployeUID is required")]
        public string EmployeUID { get; set; }
        [Required(ErrorMessage = "UpdatedBy is required")]
        public string UpdatedBy { get; set; }
    }

    public class DeleteEmploye
    {
        [Required(ErrorMessage = "EmployeUID is required")]
        public string EmployeUID { get; set; }
        [Required(ErrorMessage = "UpdatedBy is required")]
        public string UpdatedBy { get; set; }
    }
}
