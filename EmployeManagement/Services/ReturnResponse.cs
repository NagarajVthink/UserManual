using EmployeManagement.Models;

namespace EmployeManagemant.Services
{
    //Model for Apis Response
    public static class ReturnResponse
    {
        //This method will create common response.
        public static ResponseModel CreateResponse(object values, string message, bool success = false)
        {
            return new ResponseModel
            {
                Success = success,
                Data = values,
                Message = message
            };
        }
    }
}
