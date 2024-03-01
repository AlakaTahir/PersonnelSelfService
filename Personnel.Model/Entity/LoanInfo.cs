using Personnel.Model.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.Entity
{
    public class LoanInfo
    {
        public Guid Id { get; set; }
        public string EmployeeId { get; set; }
        public decimal AmountRequested { get; set; }
        public DateTime StartDate { get; set; }
        public int NoOfInstallment { get; set; }
        public DateTime EndDate { get; set; }
        public double InterestRate { get; set; }
        public decimal? AmountApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public decimal InstallmentAmount { get; set; }
        public string Reason { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public LoanStatus Status { get; set; }
    }
}
