using Personnel.Model.Entity;
using Personnel.Model.ViewModel.RequestModel;
using Personnel.Model.ViewModel.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.Service.Interface
{
    public  interface IEmployeeService
    {
        Task<BaseResponseModel> Create(EmployeeInformationRequestModel model);
        Task<EmployeeInfo> GetByEmployeeIdOrEmail(string searchParameter);
        Task<BaseResponseModel> UpdateEmployeeRecord(Guid id, EmployeeUpdateModel model);
        Task<BaseResponseModel> AssignLineManager(LineManagerRequestModel model);
        Task<bool> ActivateEmployee(Guid id);
        Task<bool> DeactivateEmployee(Guid id);
        Task<List<EmployeeResponseModel>> GetEmployeeByDepartment(string department);
        Task<int> GetEmployeeCount();
        Task<List<EmployeeResponseModel>> GetAllEmployee();

	}
}
