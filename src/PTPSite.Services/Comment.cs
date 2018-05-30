using System;

namespace PTPSite.Services
{
	public class Comment
	{
		public string Id { get; set; }

		public string Text { get; set; }

		public DateTime Date { get; set; }

		public string ByUserId { get; set; }

		public string ByUserName { get; set; }
	}
}
