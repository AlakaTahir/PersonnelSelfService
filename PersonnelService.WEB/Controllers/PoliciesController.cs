using Microsoft.AspNetCore.Mvc;

namespace PersonnelService.WEB.Controllers
{
	public class PoliciesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
