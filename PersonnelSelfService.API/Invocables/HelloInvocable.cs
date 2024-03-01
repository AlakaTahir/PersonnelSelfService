using Coravel.Invocable;
using Personnel.Service.Interface;
using System.Threading.Tasks;

namespace PersonnelSelfService.API.Invocables
{
	public class HelloInvocable: IInvocable
	{
		private readonly ILeaveService _leaveService;

		public HelloInvocable(ILeaveService leaveService)
		{
			_leaveService = leaveService;
		}

		public async Task Invoke()
		{
			await _leaveService.GenerateData();
		}
	}
}
