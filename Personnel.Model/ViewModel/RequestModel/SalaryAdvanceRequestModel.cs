using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.ViewModel.RequestModel
{
	public  class SalaryAdvanceRequestModel
	{
		public string EmployeeId { get; set; }
		public decimal AdvanceAmount { get; set; }
		public string Reason { get; set; }
	}
}
