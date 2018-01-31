using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PTPSite.Services;

namespace PTPSite.Web.Controllers
{
	[Authorize]
	[Route("comment")]
	public class CommentController : Controller
	{
		private readonly ICommentService _commentService;
		private readonly UserManager<ApplicationUser> _userManager;

		public CommentController(ICommentService commentService, UserManager<ApplicationUser> userManager)
		{
			_commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
		}

		[Route("list")]
		[AllowAnonymous]
		public async Task<IActionResult> List(CancellationToken cancellationToken = default)
		{
			Comment[] comments = await _commentService.List(cancellationToken);

			return Ok(comments);
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> Create(string text, CancellationToken cancellationToken = default)
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);

			if (user == null)
			{
				throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			var comment = new Comment
			{
				Text = text,
				Date = DateTime.Now,
				ByUserId = user.Id,
			};

			await _commentService.Create(comment, cancellationToken);

			return Ok();
		}
	}
}
