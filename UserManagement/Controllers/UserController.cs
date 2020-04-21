using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using UserManagement.Models;
using UserManagement.Models.Users;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static AppConfig _appConfig;
        public UserController(IOptions<AppConfig> appSetting)
        {
            _appConfig = appSetting.Value;
        }

        [HttpPost]
        [Route("Search")]
        public IActionResult Search(SearchRequest req)
        {
            var service = new User(_appConfig);
            return Ok(service.SearchUserDetail(req));
        }

        [HttpGet]
        [Route("SearchByID/{userUID}")]
        public IActionResult SearchByUserUID(string userUID)
        {
            if (Guid.TryParse(userUID, out _))
            {
                var service = new User(_appConfig);
                return Ok(service.SearchUserDetailByUserUID(userUID));
            }
            else
            {
                return BadRequest(ReturnResponse.CreateResponse("", "Invalid userUID : " + userUID, false));
            }
        }

        [HttpPut]
        [Route("CreateUser")]
        public IActionResult CreateUser(CreateUser user)
        {
            var service = new User(_appConfig);
            return Ok(service.InsertUserDetails(user));
        }

        [HttpPost]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(UpdateUser user)
        {
            var service = new User(_appConfig);
            return Ok(service.UpdateUserDetailByUserUID(user));
        }

        [HttpPost]
        [Route("DeleteUser")]
        public IActionResult DeleteUser(DeleteUser user)
        {
            var service = new User(_appConfig);
            return Ok(service.InActiveUserByUID(user));
        }
    }
}
