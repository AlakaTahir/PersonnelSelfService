using Microsoft.AspNetCore.Mvc;
using Personnel.Service.Interface;
using Personnel.Service.Service;
using PersonnelService.WEB.Models;
using System.Threading.Tasks;

namespace PersonnelService.WEB.Controllers
{
	public class LoanController : Controller
	{
		private readonly ILoanService _loanService;
		public LoanController(ILoanService loanService)
		{
			_loanService = loanService;
		}
		public async Task<IActionResult> Index()
		{
			var loanList = await _loanService.GetAllLoan();
			var pendinLoanCount = await _loanService.PendingLoanCount();
			var approvedLoanCount = await _loanService.ApprrovedLoanCount();
			var ongoingLoanCount = await _loanService.OngoingLoanCount();
			var completedLoanCount = await _loanService.CompletedLoanCount();
			var rejectedLoanCount = await _loanService.RejectedLoanCount();
			var overdueLoanCount = await _loanService.OverdueLoanCount();

			var loanDashboard = new LoanDashboardModel
			{
				LoanRecord = loanList,
				PendingLoan = pendinLoanCount,
				ApprovedLoan = approvedLoanCount,
				OngoingLoan = ongoingLoanCount,
				CompletedLoan = completedLoanCount,
				RejectedLoan = rejectedLoanCount,
				OverdueLoan = overdueLoanCount


			};
			return View(loanDashboard);
			
		}
	}
}
