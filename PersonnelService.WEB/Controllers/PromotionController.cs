using Microsoft.AspNetCore.Mvc;

namespace PersonnelService.WEB.Controllers
{
	public class PromotionController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
