using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.ViewModel.RequestModel
{
	public  class SalaryAdvanceRequestModel
	{
		public string EmployeeId { get; set; }
		public decimal AdvanceAmount { get; set; }
		public DateTime IssueDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool IsPaid { get; set; }
	}
}
