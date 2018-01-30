using System;

namespace PTPSite.Web.Models.CommentViewModels
{
	public class CommentViewModel
	{
		public int Id { get; set; }

		public string Text { get; set; }

		public int ByUserId { get; set; }

		public string ByUserName { get; set; }

		public DateTime Date { get; set; }
	}
}
