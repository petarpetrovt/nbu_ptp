using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTPSite.Services;
using PTPSite.Web.Infrastructure;
using PTPSite.Web.ViewModels;
using PTPSite.Web.ViewModels.Comment;
using PTPSite.Web.ViewModels.Home;

namespace PTPSite.Web.Controllers
{
	[Route("")]
	public class HomeController : BaseController
	{
		private readonly ICommentService _commentService;
		private readonly UserManager<ApplicationUser> _userManager;

		public HomeController(ICommentService commentService, UserManager<ApplicationUser> userManager)
		{
			_commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
		}

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

		[Route("discussion")]
		public async Task<IActionResult> Discussion(CancellationToken cancellationToken = default)
		{
			Comment[] comments = await _commentService.List(cancellationToken);

			CommentViewModel[] commentViewModels = comments
				.Select(x => new CommentViewModel(x))
				.ToArray();

			ApplicationUser user = await _userManager.GetUserAsync(User);

			var model = new DiscussionViewModel
			{
				Comments = commentViewModels,
				UserId = user?.Id,
			};

			return View(model);
		}
	}
}
