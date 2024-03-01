using Personnel.Model.ViewModel.RequestModel;
using Personnel.Model.ViewModel.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.Service.Interface
{
    public interface ILeaveService
    {
        Task<BaseResponseModel> Create(LeaveApplicationRequestModel model);
        Task<BaseResponseModel> ApproveLeave(Guid id, LeaveServiceApproveModel model);
        Task<BaseResponseModel> DeleteLeaveApplication(Guid id);
        Task<LeaveApplicationResponseModel> GetLeaveByEmployeeId(string employeeeId);
		Task<int> GetLeaveCount();
        Task<List<LeaveApplicationResponseModel>> GetAllLeave();
        Task<int> PendingLeaveCount();
        Task<int> ApprovedLeaveCount();
        Task<int> OngoingLeaveCount();
        Task<int> CompletedLeaveCount();
        Task<int> RejectedLeaveCount();

        Task<string> GenerateData();

	}
}
