namespace UserManagement.Models
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
        public string InsertUserDetails { get; set; }
        public string UpdateUserDetails { get; set; }
        public string SearchUserByUserID { get; set; }
        public string SearchUser { get; set; }
        public string DeleteUser { get; set; }
    }
}