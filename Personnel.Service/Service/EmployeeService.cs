using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Personnel.Model.Entity;
using Personnel.Model.ServiceModel;
using Personnel.Model.ViewModel.RequestModel;
using Personnel.Model.ViewModel.ResponseModel;
using Personnel.Service.Interface;
using Personnel.Service.Providers.Termii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.Service.Service
{
    public class EmployeeService : IEmployeeService


    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITermiiProvider _termiiProvider;

		public EmployeeService(IUnitOfWork unitOfWork, UserManager<AppIdentityUser> userManager, IMapper mapper, ITermiiProvider termiiProvider)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_termiiProvider = termiiProvider;
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
                newuser.EmployeeId = model.EmployeeId;

                _unitOfWork.GetRepository<EmployeeInfo>().Insert(newuser);
                await _unitOfWork.SaveChangesAsync();
                var noficationModel = new NotificationModel
                {
                  to = model.PhoneNumber,
                  from = "AlakaTahir",
                  sms = $"Hello {model.FirstName}, <br/><br/> Congratulations! You can now login with your detail.",
                  api_key = "",
                  channel = "dnd",
                  
                  
                };
                await _termiiProvider.SendMessage(noficationModel);

                return new BaseResponseModel
                {
                   Message = "Employee Successfully Registered",
                   Status = true
                };

                
            }

            //proceed to onboard on partner platform
           // var partnerResonse = await _provider.
            return new BaseResponseModel

            {
                Message = "Employee Already Registered",
                Status = false
            };
        }
        public async Task<EmployeeInfo> GetByEmployeeIdOrEmail(string searchParameter)
        {
            var response = await _unitOfWork.GetRepository<EmployeeInfo>().GetFirstOrDefaultAsync(predicate: x => x.EmployeeId == searchParameter || x.Email == searchParameter);
            return  response;
        }
        public async Task<BaseResponseModel> UpdateEmployeeRecord(Guid id, EmployeeUpdateModel model)
        {
            var access = _unitOfWork.GetRepository<EmployeeInfo>().GetFirstOrDefault(predicate: x => x.Id == id);
            if (access != null)
            {
                access.FirstName = model.FirstName != "" ? model.FirstName :access.FirstName;
                access.LastName = model.LastName != "" ? model.FirstName : access.LastName;
                access.DateOfBirth = model.DateOfBirth != null ? model.DateOfBirth : access.DateOfBirth;
                access.Email = model.Email != "" ? model.Email : access.Email;
                access.Address = model.Address != "" ? model.Address : access.Address;
                access.DateOfFirstAppointment = model.DateOfFirstAppointment != null ? model.DateOfFirstAppointment : access.DateOfFirstAppointment; 
                access.Department = model.Department != "" ? model.Department : access.Department;
                access.Unit = model.Unit != "" ? model.Unit : access.Unit;
                access.GradeLevel = model.GradeLevel != "" ? model.GradeLevel : access.GradeLevel;
                access.PhoneNumber = model.PhoneNumber != "" ? model.PhoneNumber : access.PhoneNumber;
                access.NextOfKin = model.NextOfKin != "" ? model.NextOfKin : access.NextOfKin;
                access.Qualification = model.Qualification != "" ? model.Qualification : access.Qualification;
                access.MaritalStatus = model.MaritalStatus != "" ? model.MaritalStatus : access.MaritalStatus;
                access.Dependant = model.Dependant != "" ? model.Dependant : access.Dependant;
                access.PreviousEmployment = model.PreviousEmployment != "" ? model.PreviousEmployment :access.PreviousEmployment;
                access.LineManager = model.LineManager != "" ? model.LineManager : access.LineManager;
                access.UpdatedDate = DateTime.Now;

                _unitOfWork.GetRepository<EmployeeInfo>().Update(access);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponseModel
                {
                    Message = "Updated Successfully",
                    Status = true
                };
            }
            return new BaseResponseModel
            {
                Message = "Unsuccessful",
                Status = false

            };
        }
        public async Task<BaseResponseModel> AssignLineManager(LineManagerRequestModel model)
        {
            var access = _unitOfWork.GetRepository<EmployeeInfo>().GetFirstOrDefault(predicate: x => x.EmployeeId == model.EmployeeId);
            if (access != null)
            {
                access.LineManager = model.LineManager != "" ? model.LineManager : access.LineManager;
                access.UpdatedDate = DateTime.Now;

                _unitOfWork.GetRepository<EmployeeInfo>().Update(access);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponseModel
                {
                    Message = " LineManager Assigned Successfully",
                    Status = true
                };
            }
            return new BaseResponseModel
            {
                Message = "Unsuccessful",
                Status = false

            };

        }

        public async Task<bool> ActivateEmployee(Guid id)
        {
            var user = _unitOfWork.GetRepository<EmployeeInfo>().GetFirstOrDefault(predicate: x => x.Id == id);
            if (user != null)
            {
                user.IsActive = true;
                user.UpdatedDate = DateTime.Now;

                _unitOfWork.GetRepository<EmployeeInfo>().Update(user);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeactivateEmployee(Guid id)
        {
            var user = _unitOfWork.GetRepository<EmployeeInfo>().GetFirstOrDefault(predicate: x => x.Id == id);
            if (user != null)
            {
                user.IsActive = false;
                user.UpdatedDate = DateTime.Now;

                _unitOfWork.GetRepository<EmployeeInfo>().Update(user);
                await _unitOfWork.SaveChangesAsync();
                return false;
            }
            return true;
        }

        public async Task<List<EmployeeResponseModel>> GetEmployeeByDepartment(string department)
        {
            var user = await _unitOfWork.GetRepository<EmployeeInfo>().GetAllAsync(predicate: x => x.Department == department);
            if (user != null)
            {
                var record = _mapper.Map<List<EmployeeResponseModel>>(user);
                return record;
            }
            else
            {
                return null;
            };
        }
		public async Task<List<EmployeeResponseModel>> GetAllEmployee()
		{
			var employees = (await _unitOfWork.GetRepository<EmployeeInfo>().GetAllAsync()).ToList();
			if (employees.Count != 0)
			{
				var response = new List<EmployeeResponseModel>();
				foreach (var employee in employees)
				{
					var singleModel = new EmployeeResponseModel();
					singleModel.FirstName = employee.FirstName;
					singleModel.LastName = employee.LastName;
					singleModel.CreatedBy = employee.CreatedBy;
                    singleModel.DateOfBirth = employee.DateOfBirth;
					singleModel.Unit = employee.Unit;
					singleModel.Department = employee.Department;
                    singleModel.GradeLevel = employee.GradeLevel;

					response.Add(singleModel);

				}
				return response;

			}
			else
			{
				return null;
			}
		}


		public async Task<int> GetEmployeeCount()
		
        {
			var count = _unitOfWork.GetRepository<EmployeeInfo>().Count();
			return count; 
		}
    }
}
