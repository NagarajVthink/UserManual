using System.ComponentModel.DataAnnotations;

namespace EmployeManagement.Models.Employees
{
    public class SearchEmployeResponse : SearchBase
    {
        public string EmployeUID { get; set; }
    }
    public class SearchRequest : SearchBase
    {
        [Required(ErrorMessage = "StartIndex is required")]
        public int StartIndex { get; set; }
        [Required(ErrorMessage = "EndIndex is required")]
        public int EndIndex { get; set; }
    }
}
