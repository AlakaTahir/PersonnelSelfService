using Personnel.Model.ViewModel.ResponseModel;
using System.Collections.Generic;

namespace PersonnelService.WEB.Models
{
	public class LoanDashboardModel
	{
		public List<LoanResponseModel> LoanRecord { get; set; }
		public int PendingLoan { get; set; }
		public int ApprovedLoan { get; set; }
		public int OngoingLoan { get; set; }
		public int CompletedLoan { get; set; }
		public int RejectedLoan { get; set; }
		public int OverdueLoan { get; set; }
	}
}
