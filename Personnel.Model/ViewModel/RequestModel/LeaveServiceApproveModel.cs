using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.ViewModel.RequestModel
{
    public class LeaveServiceApproveModel
    {
        public decimal BonusAmount { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
    }
}
