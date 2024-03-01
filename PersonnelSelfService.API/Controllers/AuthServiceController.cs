using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personnel.Model.ViewModel.RequestModel;
using Personnel.Service.Interface;
using Personnel.Service.Service;
using System.Threading.Tasks;

namespace PersonnelSelfService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthServiceController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthServiceController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// "From body" means, converting the request to Json format
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Create")]

        public async Task<IActionResult> Register([FromBody] CreateRequestViewModel model)
        {
            (bool status, string message) = await _authService.Register(model.Firstname, model.Lastname,model.PhoneNumber,model.Email,model.EmployeeId,model.Password,model.Createdby);
            
            return Ok(new
            {
                status,
                message
            });
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login(string username, string password)
        {
            (bool status, string message, string token, string refreshToken) = await _authService.Login(username,password);
            return Ok(new
            {
                status,
                message,
                token,
                refreshToken
            });
        }
        [HttpPost("Logout")]

        public async Task<IActionResult> Logout()
        {
            var response = await _authService.Logout();
            return Ok(response);
        }
        [HttpPut("ChangePassword")]

        public async Task<IActionResult> ChangePassword(string employeeid, string oldPassword, string newPassword)
        {
            var response = await _authService.ChangePassword(employeeid,oldPassword,newPassword);
            return Ok(response);
        }
    }
}
