using Personnel.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.ViewModel.ResponseModel
{
    public class LeaveApplicationResponseModel
    {
        public string EmployeeId { get; set; }
        public string LeaveTypeInfoId { get; set; }
        public LeaveType LeaveType { get; set; }
		public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double BonusAmount { get; set; }
        public string Description { get; set; }
        public int RequestedLeaveDays { get; set; }
        public DateTime? ResumptionDate { get; set; }
        public LeaveStatus Status { get; set; }

		public bool IsReviewed { get; set; }
        public bool IsApproved { get; set; }
    }
}
