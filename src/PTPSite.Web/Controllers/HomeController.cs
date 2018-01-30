﻿using System.Diagnostics;
using PTPSite.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace PTPSite.Web.Controllers
{
	public class HomeController: Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

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
