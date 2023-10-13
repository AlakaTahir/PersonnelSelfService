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
