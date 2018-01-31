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

		public CommentViewModel[] Comments { get; set; }
	}
}
