using Personnel.Model.ViewModel.RequestModel;
using Personnel.Model.ViewModel.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Personnel.Service.Interface
{
	public interface ISalaryAdvanceService
	{
		Task<BaseResponseModel> CreateSalaryAdvance(SalaryAdvanceRequestModel model);
	}
}
