using Arch.EntityFrameworkCore.UnitOfWork;
using Personnel.Model.Entity;
using Personnel.Model.ViewModel.RequestModel;
using Personnel.Model.ViewModel.ResponseModel;
using Personnel.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.Service.Service
{
    public class EmployeeService : IEmployeeService


    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //1 - why do we need many properties on the EmployeeInformationRequestModel if we are making use of just 4 of them?
        //2 - why is DateOfBirth string on the EmployeeInfo?
        //3 - EmployeeId can be string instead of int on the EmployeeInfo considering for a company using this forma "DELT001"
        public async Task<BaseResponseModel> Create (EmployeeInformationRequestModel model)
        {
            var user = await _unitOfWork.GetRepository<EmployeeInfo>().GetFirstOrDefaultAsync(predicate: x => x.EmployeeId == model.EmployeeId);
            if (user == null)
            {
                var newuser = new EmployeeInfo();
                newuser.Id = Guid.NewGuid();               
                newuser.FirstName = model.FirstName;
                newuser.LastName = model.LastName;               
                newuser.PhoneNumber = model.PhoneNumber;               
                newuser.Email = model.Email;
                newuser.CreatedBy = model.CreatedBy;
                newuser.CreatedDate = DateTime.Now;

                _unitOfWork.GetRepository<EmployeeInfo>().Insert(newuser);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponseModel
                {
                   Message = "Employee Successfully Registered",
                   Status = true
                };

                
            }
            return new BaseResponseModel

            {
                Message = "Employee Already Registered",
                Status = false
            };
        }
    }
}
