using Personnel.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.Entity
{
    public class LeaveInfo
    {
        public Guid Id { get; set; }
        public String EmployeeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double BonusAmount { get; set; }
        public int AllowedLeaveDays { get; set; }
        public int RemainingLeaveDays { get; set; }
        public bool IsApproved { get; set; }
		public string Description { get; set; }
        public int RequestedLeaveDays { get; set; }
        public LeaveType LeaveType { get; set; }
        public LeaveStatus Status { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ResumptionDate { get; set; }
    }
}
