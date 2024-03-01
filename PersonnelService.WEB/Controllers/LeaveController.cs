using Microsoft.AspNetCore.Mvc;
using Personnel.Model.Enumeration;
using Personnel.Service.Interface;
using PersonnelService.WEB.Models;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PersonnelService.WEB.Controllers
{
	public class LeaveController : Controller
	{
		private readonly ILeaveService _leaveService;

		public LeaveController(ILeaveService leaveService)
		{
			_leaveService = leaveService;
		}

		public async Task<IActionResult> Index()
		{
			var leaveList = await _leaveService.GetAllLeave();
			var pendinglist = await _leaveService.PendingLeaveCount();
			var approvedlist = await _leaveService.ApprovedLeaveCount();
			var ongoinglist = await _leaveService.OngoingLeaveCount();
			var completedlist = await _leaveService.CompletedLeaveCount();
			var rejectedlist = await _leaveService.RejectedLeaveCount();

			var leaveDashboard = new LeaveDashboardModel
			{
				LeaveRecord = leaveList,
				PendingLeave = pendinglist,
				ApprovedLeave = approvedlist,
				OngoingLeave = ongoinglist,
				CompletedLeave = completedlist,
				RejectedLeave = rejectedlist
			};
			return View(leaveDashboard);
		}
		[HttpPost]
		public async Task<IActionResult> ApplyLeave(string leaveTypeId, string dateFrom,string dateTo, int noOfDays, int remainigDays,string leaveReason)
		{
			return Ok(new
			{
				status = true,
				message = "Successful"
			});
		}
		
	}
}
