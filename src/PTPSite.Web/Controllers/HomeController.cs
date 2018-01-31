using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PTPSite.Web.Models;

namespace PTPSite.Web.Controllers
{
	[Route("")]
	public class HomeController : Controller
	{
		[Route("")]
		public IActionResult Index()
		{
			return View();
		}

		[Route("experience")]
		public IActionResult Experience()
		{
			return View();
		}

		[Route("education")]
		public IActionResult Education()
		{
			return View();
		}

		[Route("skills")]
		public IActionResult Skills()
		{
			return View();
		}

		[Route("interests")]
		public IActionResult Interests()
		{
			return View();
		}

		[Route("certifications")]
		public IActionResult Certifications()
		{
			return View();
		}

		[Route("error")]
		public IActionResult Error()
		{
			var viewModel = new ErrorViewModel
			{
				RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
			};

			return View(viewModel);
		}
	}
}
