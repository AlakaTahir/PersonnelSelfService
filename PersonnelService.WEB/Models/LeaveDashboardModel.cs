using Personnel.Model.ViewModel.ResponseModel;
using System.Collections.Generic;

namespace PersonnelService.WEB.Models
{
	public class LeaveDashboardModel
	{
		public List<LeaveApplicationResponseModel> LeaveRecord { get; set; }
		public int PendingLeave { get; set; }
		public int ApprovedLeave { get; set; }
		public int OngoingLeave { get; set; }
		public int CompletedLeave { get; set; }
		public int RejectedLeave { get; set; }
		public int OverdueLeave { get; set; }
	}
}

