using System;

namespace EmployeManagement.Models
{
    public class EmployeException : Exception
    {
        public EmployeException(string message) : base(message) { }
        public EmployeException(string message, Exception innerException) : base(message, innerException) { }
    }
}
