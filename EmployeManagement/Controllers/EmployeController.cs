using EmployeManagemant.Services;
using EmployeManagement.Models;
using EmployeManagement.Models.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace EmployeManagemant.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeController : ControllerBase
    {
        public static AppConfig _appConfig;
        public EmployeController(IOptions<AppConfig> appSetting)
        {
            _appConfig = appSetting.Value;
        }

        [HttpPost]
        [Route("Search")]
        public IActionResult Search(SearchRequest req)
        {
            var service = new Employe(_appConfig);
            return Ok(service.SearchEmployeDetail(req));
        }

        [HttpGet]
        [Route("SearchByID/{employeUID}")]
        public IActionResult SearchByEmployeUID(string employeUID)
        {
            if (Guid.TryParse(employeUID, out _))
            {
                var service = new Employe(_appConfig);
                return Ok(service.SearchEmployeDetailByEmployeUID(employeUID));
            }
            else
            {
                return BadRequest(ReturnResponse.CreateResponse("", "Invalid employeUID : " + employeUID, false));
            }
        }

        [HttpPut]
        [Route("CreateEmploye")]
        public IActionResult CreateEmploye(CreateEmploye employe)
        {
            var service = new Employe(_appConfig);
            return Ok(service.InsertEmployeDetails(employe));
        }

        [HttpPost]
        [Route("UpdateEmploye")]
        public IActionResult UpdateEmploye(UpdateEmploye employe)
        {
            var service = new Employe(_appConfig);
            return Ok(service.UpdateEmployeDetailByEmployeUID(employe));
        }

        [HttpPost]
        [Route("DeleteEmploye")]
        public IActionResult DeleteEmploye(DeleteEmploye employe)
        {
            var service = new Employe(_appConfig);
            return Ok(service.InActiveEmployeByUID(employe));
        }
    }
}
