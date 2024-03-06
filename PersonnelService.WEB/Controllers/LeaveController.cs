using Microsoft.AspNetCore.Mvc;
using Personnel.Model.Enumeration;
using Personnel.Service.Interface;
using PersonnelService.WEB.Models;
using System.Threading.Tasks;
using Personnel.Model.ViewModel.RequestModel;
using System;
using System.Globalization;

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
		public async Task<IActionResult> ApplyLeave(int leaveTypeId, string dateFrom,string dateTo, int noOfDays, int remainigDays,string leaveReason)
		{
			var model = new LeaveApplicationRequestModel
			{
				EmployeeId = "seyi001",
				LeaveType = (LeaveType)leaveTypeId,
				DateFrom = DateTime.ParseExact(dateFrom, "yyyy-MM-dd HH:mm:ss,fff", CultureInfo.InvariantCulture),
				NoOfDaysAppliedFor = noOfDays,
				Description = leaveReason,
				BonusAmount = 10000
			};

			var leaveResponse = _leaveService.Create(model);
			return Ok(new
			{
				status = true,
				message = "Successful"
			});
		}
		
	}
}
