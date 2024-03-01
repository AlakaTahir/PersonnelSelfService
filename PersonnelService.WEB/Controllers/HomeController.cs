using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Personnel.Service.Interface;
using PersonnelService.WEB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelService.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly ILoanService _loanService;
        private readonly ILeaveService _leaveService;

		public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService, ILoanService loanService, ILeaveService leaveService)
		{
			_logger = logger;
			_employeeService = employeeService;
			_loanService = loanService;
			_leaveService = leaveService;
		}

		public async Task<IActionResult> Index()
        {
            var employeeNumber = await _employeeService.GetEmployeeCount(); 
            var loanNumber = await _loanService.GetLoanCount();
            var leaveNumber = await _leaveService.GetLeaveCount();
            
            

            var homeModel = new HomeModel
            {
                EmployeeCount = employeeNumber,
                LoanCount = loanNumber,
                LeaveCount = leaveNumber
                
            };

            return View(homeModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
