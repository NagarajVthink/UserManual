namespace EmployeManagement.Models
{
    public class AppConfig
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public GeneralConfig GeneralConfig { get; set; }
        public StoredProcedures StoredProcedures { get; set; }
    }
    //Connection Strings to connect DataBase
    public class ConnectionStrings
    {
        public string ClientPortal { get; set; }
    }
    public class GeneralConfig
    {
    }
    public class StoredProcedures
    {
        public string InsertEmployeDetails { get; set; }
        public string UpdateEmployeDetails { get; set; }
        public string SearchEmployeByEmployeID { get; set; }
        public string SearchEmploye { get; set; }
        public string DeleteEmploye { get; set; }
    }
}