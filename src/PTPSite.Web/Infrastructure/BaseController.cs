using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PTPSite.Web.Controllers;

namespace PTPSite.Web.Infrastructure
{
	public class BaseController : Controller
	{
		protected void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
		}

		protected IActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}
		}

		protected IActionResult Refresh()
		{
			string referrer = Request.Headers[HeaderNames.Referer];

			return Redirect(referrer);
		}
	}
}
