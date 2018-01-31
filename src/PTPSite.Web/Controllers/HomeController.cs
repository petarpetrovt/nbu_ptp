using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

		public HomeController(ICommentService commentService)
		{
			_commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
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

			var model = new DiscussionViewModel
			{
				Comments = commentViewModels,
			};

			return View(model);
		}
	}
}
