using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.Users
{
    public class SearchUsersResponse : SearchBase
    {
        public string UserUID { get; set; }
    }
    public class SearchRequest :SearchBase
    {
        [Required(ErrorMessage = "StartIndex is required")]
        public int StartIndex { get; set; }
        [Required(ErrorMessage = "EndIndex is required")]
        public int EndIndex { get; set; }
    }
}
