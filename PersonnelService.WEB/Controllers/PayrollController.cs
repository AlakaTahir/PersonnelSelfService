using Microsoft.AspNetCore.Mvc;

namespace PersonnelService.WEB.Controllers
{
	public class PayrollController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
