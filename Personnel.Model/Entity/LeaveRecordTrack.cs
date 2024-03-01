using Personnel.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.Entity
{
	public class LeaveRecordTrack
	{
		public string EmployeeId { get; set; }
		public int Year { get; set; }
		public int DaysTaken { get; set; }
		public LeaveType LeaveType { get; set; }
	}
}
