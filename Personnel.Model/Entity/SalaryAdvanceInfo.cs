using Personnel.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.Entity
{
	public class SalaryAdvanceInfo
	{
		public Guid Id { get; set; }
		public string EmployeeId { get; set; }
		public decimal AdvanceAmount { get; set; }
		public DateTime CreatedDate { get; set; }
		public string SalaryAdvanceId { get; set; }
		public string Reason { get; set; }
		public SalaryAdvanceStatus Status { get; set; }
	}
}

