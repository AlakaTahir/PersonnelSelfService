using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.ViewModel.ResponseModel
{
	public class DisciplinaryResponseModel
	{
		public string Department { get; set; }
		public string GradeLevel { get; set; }
		public string ActionType { get; set; }
		public string Reason { get; set; }
		public DateTime? Date { get; set; }
	}
}
