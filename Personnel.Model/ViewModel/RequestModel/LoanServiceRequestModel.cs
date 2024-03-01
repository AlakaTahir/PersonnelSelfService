using Personnel.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.ViewModel.RequestModel
{
    public class LoanServiceRequestModel
    {
        public string EmployeeId { get; set; }
        public decimal AmountRequested { get; set; }
        public DateTime StartDate { get; set; }
        public string Reason { get; set; }
        public int NoOfInstallment { get; set; }
        
    }
}
