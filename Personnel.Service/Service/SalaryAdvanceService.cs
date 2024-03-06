using Arch.EntityFrameworkCore.UnitOfWork;
using Personnel.Model.Entity;
using Personnel.Model.Enumeration;
using Personnel.Model.ViewModel.RequestModel;
using Personnel.Model.ViewModel.ResponseModel;
using Personnel.Service.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.Service.Service
{
	public class SalaryAdvanceService :ISalaryAdvanceService
	{
		public readonly IUnitOfWork _unitOfWork;
		public SalaryAdvanceService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public bool IsEligibleForSalaryAdvance(DateTime dateOfFirstAppointment)
		{

			if (DateTime.Now.AddYears(-3) >= dateOfFirstAppointment)

			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public async Task<BaseResponseModel> CreateSalaryAdvance(SalaryAdvanceRequestModel model)
		{
			var user = await _unitOfWork.GetRepository<EmployeeInfo>().GetFirstOrDefaultAsync(predicate: x => x.EmployeeId == model.EmployeeId);

			if (user != null)
			{
				// Check eligibility for salary advance
				var isEligibleforSalaryAdvance = IsEligibleForSalaryAdvance(user.DateOfFirstAppointment.Value);
				if (isEligibleforSalaryAdvance)
				{
					var advance = new SalaryAdvanceInfo();
					advance.Id = Guid.NewGuid();
					advance.EmployeeId = model.EmployeeId;
					advance.AdvanceAmount = model.AdvanceAmount;
					advance.Reason = model.Reason;
					advance.CreatedDate = DateTime.Now;
					advance.Status = SalaryAdvanceStatus.Pending;

					
					_unitOfWork.GetRepository<SalaryAdvanceInfo>().Insert(advance);
					await _unitOfWork.SaveChangesAsync();
					return new BaseResponseModel
					{
						Message = "Salary Advance Created Successfully",
						Status = true
					};

				}
				return new BaseResponseModel
				{
					Message = "You are not Eligible",
					Status = false
				};

			}
			return new BaseResponseModel
			{
				Message = "Employee Does not Exist",
				Status = false
			};

		}

		public async Task<BaseResponseModel> ApproveSalaryAdvance(Guid id, SalaryAdvanceApprovalModel model)
		{
			var advance = _unitOfWork.GetRepository<SalaryAdvanceInfo>().GetFirstOrDefault(predicate: x => x.Id == id);
			if (advance != null)
			{

				
				advance.Status = model.Status;
				

				_unitOfWork.GetRepository<SalaryAdvanceInfo>().Update(advance);
				await _unitOfWork.SaveChangesAsync();

				return new BaseResponseModel
				{
					Message = "Your Salary Advance is Approved",
					Status = true
				};
			}
			return new BaseResponseModel
			{
				Message = "Your Salary Advance Is not Approved",
				Status = false

			};
		}


	}
}
