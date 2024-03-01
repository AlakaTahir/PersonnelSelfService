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
		public DateTime IssueDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool IsPaid { get; set; }
	}
}

