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

namespace Personnel.Service.Service
{
    public class LeaveService : ILeaveService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LeaveService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponseModel> Create(LeaveApplicationRequestModel model)
        {
            var annualleave = 30;
            var sickLeave = 10;

			var employee = await _unitOfWork.GetRepository<EmployeeInfo>().GetFirstOrDefaultAsync(predicate: x => x.EmployeeId == model.EmployeeId);
            if (employee != null)
            {
				var leaveTrack = await _unitOfWork.GetRepository<LeaveRecordTrack>().GetFirstOrDefaultAsync(predicate: x => x.EmployeeId == model.EmployeeId && x.Year == model.DateFrom.Year && x.LeaveType == model.LeaveType );
                if (leaveTrack != null)
                {
                    if(model.LeaveType ==  LeaveType.AnnualLeave && annualleave - leaveTrack.DaysTaken <= 0)
                    {
						return new BaseResponseModel
						{
							Message = "Employee not eligible for another leave",
							Status = false
						};
					}
                    else if(model.LeaveType == LeaveType.SickLeave && sickLeave - leaveTrack.DaysTaken <= 0)
                    {
						return new BaseResponseModel
						{
							Message = "Employee not eligible for another leave",
							Status = false
						};
					}
				}

				var leave = new LeaveInfo();
				leave.Id = Guid.NewGuid();
				leave.EmployeeId = model.EmployeeId;
				leave.DateFrom = model.DateFrom;
				leave.DateTo = AddBusinessDays(model.DateFrom, model.NoOfDaysAppliedFor);
                leave.RequestedLeaveDays = leave.RequestedLeaveDays;
				leave.ResumptionDate = AddBusinessDays(model.DateFrom, model.NoOfDaysAppliedFor + 1);
				leave.Description = model.Description;
				leave.LeaveType = model.LeaveType;
				leave.Status = LeaveStatus.Pending;
				leave.CreatedDate = DateTime.Now;

				_unitOfWork.GetRepository<LeaveInfo>().Insert(leave);
				await _unitOfWork.SaveChangesAsync();
				return new BaseResponseModel
				{
					Message = "Leave Created Successfully",
					Status = true
				};
			}
            return new BaseResponseModel
            {
                Message = "Employee does not exist",
                Status = false
            };
        }

        public async Task<BaseResponseModel> ApproveLeave(Guid id, LeaveServiceApproveModel model)
        {
            var leave = _unitOfWork.GetRepository<LeaveInfo>().GetFirstOrDefault(predicate: x => x.Id == id);
            if (leave != null)
            {
                leave.BonusAmount = model.BonusAmount;
				leave.AllowedLeaveDays = 30;
				leave.RequestedLeaveDays = leave.RequestedLeaveDays;
				leave.Status = LeaveStatus.Approved;
                leave.UpdatedDate = DateTime.Now;
                leave.ApprovedBy = model.ApprovedBy;
                leave.ApprovedDate = DateTime.Now;

                _unitOfWork.GetRepository<LeaveInfo>().Update(leave);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponseModel
                {
                    Message = "Your Leave is Approved",
                    Status = true
                };
            }
            return new BaseResponseModel
            {
                Message = "Your Leave is not Approved",
                Status = false

            };
        }

        /// <summary>
        /// 1- Prevent deleteing leave without passing some certain condition
        /// 2- Make the system to determine/get the remaining leave day
        /// 3- Prevent employee from requesting more than eligible leave
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BaseResponseModel> DeleteLeaveApplication(Guid id)
        {
            var leave = _unitOfWork.GetRepository<LeaveInfo>().GetFirstOrDefault(predicate: x => x.Id == id);
            if (leave != null)
            {
                _unitOfWork.GetRepository<LeaveInfo>().Delete(leave);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponseModel
                {
                    Message = "Deleted Successfully",
                    Status = true,
                };
            }
            return null;


        }
        
        public async Task<LeaveApplicationResponseModel> GetLeaveByEmployeeId(string employeeeId)
        {
            var leave = _unitOfWork.GetRepository<LeaveInfo>().GetFirstOrDefault(predicate: x => x.EmployeeId == employeeeId);
            if (leave != null)
            {

                return new LeaveApplicationResponseModel
                {


                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now,
					RequestedLeaveDays = leave.RequestedLeaveDays,
                    BonusAmount = leave.BonusAmount,
                };
            }
            else
            {
                return null;
            };
        }

		public async Task<int> GetLeaveCount()

		{
			var count = _unitOfWork.GetRepository<LeaveInfo>().Count();
			return count;
		}
		public async Task<List<LeaveApplicationResponseModel>> GetAllLeave()
		{
			var leaves = (await _unitOfWork.GetRepository<LeaveInfo>().GetAllAsync()).ToList(); //turning the response from enumerable to list
			if (leaves.Count != 0)
			{
				var response = new List<LeaveApplicationResponseModel>();
				foreach (var leave in leaves)
				{
					var singleModel = new LeaveApplicationResponseModel();
					singleModel.DateFrom = leave.DateFrom;
					singleModel.DateTo = leave.DateTo;
					singleModel.BonusAmount = leave.BonusAmount;
					singleModel.Description = leave.Description;
					singleModel.RequestedLeaveDays = leave.RequestedLeaveDays;
                    singleModel.IsApproved = leave.IsApproved;
                    singleModel.ResumptionDate = leave.ResumptionDate;
                    singleModel.LeaveType = leave.LeaveType;
                    singleModel.Status = leave.Status;

					response.Add(singleModel);

				}
				return response;

			}
			else
			{
				return null;
			}
		}
		public async Task<int> PendingLeaveCount()

		{
			var count =  _unitOfWork.GetRepository<LeaveInfo>().Count(x => x.Status == LeaveStatus.Pending);
			return count;
		}
		public async Task<int> ApprovedLeaveCount()

		{
			var count = _unitOfWork.GetRepository<LeaveInfo>().Count(x => x.Status == LeaveStatus.Approved);
			return count;
		}
		public async Task<int> OngoingLeaveCount()

		{
			var count = _unitOfWork.GetRepository<LeaveInfo>().Count(x => x.Status == LeaveStatus.Ongoing);
			return count;
		}
		public async Task<int> CompletedLeaveCount()

		{
			var count = _unitOfWork.GetRepository<LeaveInfo>().Count(x => x.Status == LeaveStatus.Completed);
			return count;
		}
		public async Task<int> RejectedLeaveCount()

		{
			var count = _unitOfWork.GetRepository<LeaveInfo>().Count(x => x.Status == LeaveStatus.Rejected);
			return count;
		}

		private static DateTime AddBusinessDays(DateTime date, int days)
        {
            if (days < 0)
            {
                throw new ArgumentException("days cannot be negative", "days");
            }

            if (days == 0) return date;

            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                date = date.AddDays(2);
                days -= 1;
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
                days -= 1;
            }

            date = date.AddDays(days / 5 * 7);
            int extraDays = days % 5;

            if ((int)date.DayOfWeek + extraDays > 5)
            {
                extraDays += 2;
            }

            return date.AddDays(extraDays);

        }

        public async Task<string> GenerateData()
        {
            return "Hello";
        }

    }
}
