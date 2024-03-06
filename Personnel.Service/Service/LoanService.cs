
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Personnel.Model.Entity;
using Personnel.Model.Enumeration;
using Personnel.Model.ViewModel.RequestModel;
using Personnel.Model.ViewModel.ResponseModel;
using Personnel.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Personnel.Service.Service
{
    public class LoanService : ILoanService
    {   
        private readonly IUnitOfWork _unitOfWork;
        public LoanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CheckEligibility(DateTime dateOfFirstAppointment)
        {
            
            if (DateTime.Now.AddYears(-2) >= dateOfFirstAppointment)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<BaseResponseModel> Create(LoanServiceRequestModel model)
        {
            var user = await _unitOfWork.GetRepository<EmployeeInfo>().GetFirstOrDefaultAsync(predicate: x => x.EmployeeId == model.EmployeeId);
             
            if (user != null) 
            {
                if(user.DateOfFirstAppointment == null)
                {
                    return new BaseResponseModel
                    {
                        Message = "Please update your profile",
                        Status = false
                    };
                }
                var checkEligibility = CheckEligibility(user.DateOfFirstAppointment.Value);
                if (checkEligibility)               
                {
                    var newLoan = new LoanInfo();
                    newLoan.Id = Guid.NewGuid();
                    newLoan.EmployeeId = model.EmployeeId;
                    newLoan.AmountRequested = model.AmountRequested; 
                    newLoan.Reason = model.Reason;
                    newLoan.StartDate = model.StartDate;
                    newLoan.NoOfInstallment = model.NoOfInstallment;
                    newLoan.EndDate = model.StartDate.AddMonths(model.NoOfInstallment);
                    newLoan.InstallmentAmount = model.AmountRequested / model.NoOfInstallment;
                    newLoan.CreatedDate = DateTime.Now;
                    newLoan.InterestRate = 0.15;
                    newLoan.Status = LoanStatus.Pending;
                    


                    _unitOfWork.GetRepository<LoanInfo>().Insert(newLoan);
                    await _unitOfWork.SaveChangesAsync();
                    return new BaseResponseModel
                    {
                        Message = "Loan Created Successfully",
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
                Message = "Employee Already Exist",
                Status = false
            };
        }

        public async Task<BaseResponseModel> ApproveLoan(Guid id, LoanServiceApproveModel model)
        {
            var loan = _unitOfWork.GetRepository<LoanInfo>().GetFirstOrDefault(predicate: x => x.Id == id);
            if (loan != null)
            {
                
                loan.StartDate = model.StartDate;
                loan.NoOfInstallment = model.NoOfInstallment;
                loan.EndDate = model.StartDate.AddMonths(model.NoOfInstallment);
                loan.ApprovedDate = DateTime.Now;
                loan.InterestRate = 15/100;
                loan.AmountApproved = model.AmountApproved;
                loan.Status = LoanStatus.Approved;
                var interestAmount = (15/100) * loan.AmountApproved;
                loan.InstallmentAmount = (model.AmountApproved.Value + interestAmount.Value)/ model.NoOfInstallment;
                loan.ApprovedBy = model.ApprovedBy;
                loan.ApprovedDate = DateTime.Now;
                loan.ModifiedBy = model.ModifiedBy;
                loan.ModifiedDate = DateTime.Now;

                _unitOfWork.GetRepository<LoanInfo>().Update(loan);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponseModel
                {
                    Message = "Your Loan is Approved",
                    Status = true
                };
            }
            return new BaseResponseModel
            {
                Message = "Your Loan Is not Approved",
                Status = false

            };
        }

        public async Task<List<LoanResponseModel>> GetAllLoan()
        {
            var loans = (await _unitOfWork.GetRepository<LoanInfo>().GetAllAsync()).ToList();
			if (loans.Count != 0)
			{
				var response = new List<LoanResponseModel>();
				foreach (var loan in loans)
				{
					var singleModel = new LoanResponseModel();
					singleModel.AmountRequested = loan.AmountRequested;
					singleModel.StartDate = loan.StartDate;
					singleModel.NoOfInstallment = loan.NoOfInstallment;
					singleModel.EndDate = loan.EndDate;
					singleModel.InterestRate = loan.InterestRate;
					singleModel.AmountApproved = loan.AmountApproved;
					singleModel.ApprovedDate = loan.ApprovedDate;
					singleModel.StartDate = loan.StartDate;
					singleModel.InstallmentAmount = loan.InstallmentAmount;
                    singleModel.Reason = loan.Reason;
					singleModel.ApprovedBy = loan.ApprovedBy;
					singleModel.Status= loan.Status;



					response.Add(singleModel);

				}
				return response;

			}
			else
			{
				return null;
			}
		}
		public async Task<int> GetLoanCount()

		{
			var count = _unitOfWork.GetRepository<LoanInfo>().Count();
			return count;
		}
		public async Task<int> PendingLoanCount()

		{
			var count = _unitOfWork.GetRepository<LoanInfo>().Count(x => x.Status == LoanStatus.Pending);
			return count;
		}
		public async Task<int> ApprrovedLoanCount()

		{
			var count = _unitOfWork.GetRepository<LoanInfo>().Count(x => x.Status == LoanStatus.Approved);
			return count;
		}
		public async Task<int> OngoingLoanCount()

		{
			var count = _unitOfWork.GetRepository<LoanInfo>().Count(x => x.Status == LoanStatus.Ongoing);
			return count;
		}
		public async Task<int> CompletedLoanCount()

		{
			var count = _unitOfWork.GetRepository<LoanInfo>().Count(x => x.Status == LoanStatus.Completed);
			return count;
		}
		public async Task<int> RejectedLoanCount()

		{
			var count = _unitOfWork.GetRepository<LoanInfo>().Count(x => x.Status == LoanStatus.Rejected);
			return count;
		}
		public async Task<int> OverdueLoanCount()

		{
			var count = _unitOfWork.GetRepository<LoanInfo>().Count(x => x.Status == LoanStatus.Overdue);
			return count;
		}


	}
}
