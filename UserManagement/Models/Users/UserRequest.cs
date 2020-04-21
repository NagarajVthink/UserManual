using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.Users
{
    public abstract class UserBase
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

    public class CreateUser : UserBase
    {
        [Required(ErrorMessage = "CreatedBy is required")]
        public string CreatedBy { get; set; }
    }

    public class UpdateUser : UserBase
    {
        [Required(ErrorMessage = "UserUID is required")]
        public string UserUID { get; set; }
        [Required(ErrorMessage = "UpdatedBy is required")]
        public string UpdatedBy { get; set; }
    }

    public class DeleteUser
    {
        [Required(ErrorMessage = "UserUID is required")]
        public string UserUID { get; set; }
        [Required(ErrorMessage = "UpdatedBy is required")]
        public string UpdatedBy { get; set; }
    }
}
