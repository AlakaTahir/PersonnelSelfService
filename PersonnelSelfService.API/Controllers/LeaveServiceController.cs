using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel.Model.ViewModel.RequestModel;
using Personnel.Service.Interface;
using Personnel.Service.Service;
using System;
using System.Threading.Tasks;

namespace PersonnelSelfService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveServiceController : ControllerBase
    {
        private readonly ILeaveService _leaveService;
        public LeaveServiceController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }
        [HttpPost("CreateLeave")]

        public async Task<IActionResult> Create(LeaveApplicationRequestModel model)
        {
            var response = await _leaveService.Create(model);
            return Ok(response);
        }
        
        [HttpPut("ApproveLeave")]
        public async Task<IActionResult> ApproveLeave(Guid id, LeaveServiceApproveModel model)
        {
            var response = await _leaveService.ApproveLeave(id, model);
            return Ok(response);
        }
        
        [HttpDelete("DeleteLeave")]
        public async Task<IActionResult> DeleteLeaveApplication(Guid id)
        {
            var response = await _leaveService.DeleteLeaveApplication(id);
            return Ok(response);
        }
        [HttpGet("GetApplicationByUserId")]
        public async Task<IActionResult> GetLeaveByEmployeeId(string employeeeId)
        {
            var response = await _leaveService.GetLeaveByEmployeeId(employeeeId);
            return Ok(response);
        }
    }
}
