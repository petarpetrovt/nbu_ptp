using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PTPSite.Services;
using PTPSite.Web.Models.AccountViewModels;

namespace PTPSite.Web.Controllers
{
	[Authorize]
	[Route("[controller]/[action]")]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IUserService _userService;
		private readonly ILogger _logger;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IUserService userService,
			ILogger<AccountController> logger)
		{
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
			_userService = userService ?? throw new ArgumentNullException(nameof(userService));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[TempData]
		public string ErrorMessage { get; set; }

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Login(string returnUrl = null)
		{
			// Clear the existing external cookie to ensure a clean login process
			await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(
			LoginViewModel model,
			string returnUrl = null,
			CancellationToken cancellationToken = default)
		{
			ViewData["ReturnUrl"] = returnUrl;

			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					_logger.LogInformation("User logged in.");
					return RedirectToLocal(returnUrl);
				}

				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
				return View(model);
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register(string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;

			var model = new RegisterViewModel
			{
			};

			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;

			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = model.Email,
					Email = model.Email,
					Role = ApplicationRole.None,
					Name = model.Name,
				};

				IdentityResult result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					_logger.LogInformation("User created a new account with password.");

					// Refresh user to get the ID
					user = await _userManager.FindByNameAsync(user.UserName);

					await _signInManager.SignInAsync(user, isPersistent: false);

					_logger.LogInformation("User created a new account with password.");

					return RedirectToLocal(returnUrl);
				}

				AddErrors(result);
			}

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			_logger.LogInformation("User logged out.");
			return RedirectToAction(nameof(HomeController.Index), "Home");
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}

		#region Helpers

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
		}

		private IActionResult RedirectToLocal(string returnUrl)
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

		#endregion
	}
}
