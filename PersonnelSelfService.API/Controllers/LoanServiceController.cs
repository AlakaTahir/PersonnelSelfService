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
    public class LoanServiceController : ControllerBase
    {
        private readonly ILoanService _loanService;
        public LoanServiceController(ILoanService loanservice)
        {
            _loanService = loanservice;
        }
        [HttpPost("CreateLoan")]

        public async Task<IActionResult> Create(LoanServiceRequestModel model)
        {
            var response = await _loanService.Create(model);
            return Ok(response);
        }

        [HttpPut("ApproveLoan")]
        public async Task<IActionResult> ApproveLoan(Guid id, LoanServiceApproveModel model)
        {
            var response = await _loanService.ApproveLoan(id, model);
            return Ok(response);
        }
    }
}
