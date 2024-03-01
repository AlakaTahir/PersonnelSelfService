using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel.Model.ViewModel.RequestModel;
using Personnel.Service.Interface;
using System;
using System.Threading.Tasks;

namespace PersonnelSelfService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelServiceController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public PersonnelServiceController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("Create")]

        public async Task<IActionResult> Create(EmployeeInformationRequestModel model)
        {
            var response = await _employeeService.Create(model);
            return Ok(response);
        }
        [HttpPost("GetByEmployeeIdOrEmail")]
        public async Task<IActionResult> GetByEmployeeIdOrEmail(string searchParameter)
        {
            var response = await _employeeService.GetByEmployeeIdOrEmail(searchParameter);
            return Ok(response);
        }

        [HttpPut("UpdateRecord")]
        public async Task<IActionResult> UpdateEmployeeRecord(Guid id, EmployeeUpdateModel model)
        {
            var response = await _employeeService.UpdateEmployeeRecord(id,model);
            return Ok(response);
        }
        [HttpPut("AssignLineManager")]
        public async Task<IActionResult> AssignLineManager(LineManagerRequestModel model)
        {
            var response = await _employeeService.AssignLineManager(model);
            return Ok(response);
        }

        [HttpPost("ActivateEmployee")]
        public async Task<IActionResult> ActivateEmployee(Guid id)
        {
            var response = await _employeeService.ActivateEmployee(id);
            return Ok(response);
        }
        [HttpPost("DeactivateEmployee")]
        public async Task<IActionResult> DeactivateEmployee(Guid id)
        {
            var response = await _employeeService.DeactivateEmployee(id);
            return Ok(response);
        }
        [HttpPost("GetByEmployeeByDepartment")]
        public async Task<IActionResult> GetEmployeeByDepartment(string department)
        {
            var response = await _employeeService.GetEmployeeByDepartment(department);
            return Ok(response);
        }
    }
}
