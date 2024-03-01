using Arch.EntityFrameworkCore.UnitOfWork;
using Personnel.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Service.Service
{
	public class SalaryAdvanceService :ISalaryAdvanceService
	{
		public readonly IUnitOfWork _unitOfWork;
		public SalaryAdvanceService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

	}
}
