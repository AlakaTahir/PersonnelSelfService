using Personnel.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.ViewModel.RequestModel
{
    public class LoanServiceApproveModel
    {
        public decimal AmountRequested { get; set; }
        public DateTime StartDate { get; set; }
        public int NoOfInstallment { get; set; }
        public decimal? AmountApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }


    }
}
