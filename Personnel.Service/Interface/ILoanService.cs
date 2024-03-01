using Personnel.Model.ViewModel.RequestModel;
using Personnel.Model.ViewModel.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.Service.Interface
{
    public interface ILoanService
    {
        Task<BaseResponseModel> Create(LoanServiceRequestModel model);
        Task<BaseResponseModel> ApproveLoan(Guid id, LoanServiceApproveModel model);
        Task<List<LoanResponseModel>> GetAllLoan();
		Task<int> GetLoanCount();
        Task<int> PendingLoanCount();
        Task<int> ApprrovedLoanCount();
		Task<int> OngoingLoanCount();
        Task<int> CompletedLoanCount();
        Task<int> RejectedLoanCount();
        Task<int> OverdueLoanCount();


	}
}
