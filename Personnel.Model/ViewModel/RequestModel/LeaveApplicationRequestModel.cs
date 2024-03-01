using Personnel.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.ViewModel.RequestModel
{
    public class LeaveApplicationRequestModel
    {
        public string EmployeeId { get; set; }
        public DateTime DateFrom { get; set; }
        public double BonusAmount { get; set; }
        public string Description { get; set; }
        public int NoOfDaysAppliedFor { get; set; }
        public LeaveType LeaveType { get; set; }

    }
}
