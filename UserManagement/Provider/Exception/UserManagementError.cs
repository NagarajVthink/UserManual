using System;

namespace UserManagement.Models
{
    public class UserManagementError
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
