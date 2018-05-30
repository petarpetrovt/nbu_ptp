using System;
using SERVICES = PTPSite.Services;

namespace PTPSite.Web.ViewModels.Comment
{
	public class CommentViewModel
	{
		public string Id { get; set; }

		public string Text { get; set; }

		public string ByUserId { get; set; }

		public string ByUserName { get; set; }

		public DateTime Date { get; set; }

		public CommentViewModel()
		{

		}

		public CommentViewModel(SERVICES.Comment comment)
		{
			Id = comment.Id;
			Text = comment.Text;
			ByUserId = comment.ByUserId;
			ByUserName = comment.ByUserName;
			Date = comment.Date;
		}
	}
}
