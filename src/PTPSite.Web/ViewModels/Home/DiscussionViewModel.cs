using System.ComponentModel.DataAnnotations;
using PTPSite.Web.ViewModels.Comment;

namespace PTPSite.Web.ViewModels.Home
{
	public class DiscussionViewModel
	{
		[Required]
		[Display(Name = "Text")]
		[StringLength(256, MinimumLength = 10)]
		public string Text { get; set; }

		[Required]
		[Display(Name = "Text")]
		[StringLength(256, MinimumLength = 10)]
		public string TextEdit { get; set; }

		public int? UserId { get; set; }

		public CommentViewModel[] Comments { get; set; }
	}
}
