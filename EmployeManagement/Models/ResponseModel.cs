namespace EmployeManagement.Models
{
    /// <summary>
    /// This model will used as the default return type for the Contollers.
    /// </summary>
    public class ResponseModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
    }
}
