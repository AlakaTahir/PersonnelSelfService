using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.ViewModel.ResponseModel
{
	public class SalaryAdvanceResponseModel
	{
		public decimal AdvanceAmount { get; set; }
		public DateTime IssueDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public string Reason { get; set; }
		

	}
}
