using Microsoft.AspNetCore.Mvc;

namespace PersonnelService.WEB.Controllers
{
	public class ProfileController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
