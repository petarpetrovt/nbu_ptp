using System;

namespace PTPSite.Services
{
	public class Comment
	{
		public int Id { get; set; }

		public string Text { get; set; }

		public DateTime Date { get; set; }

		public int ByUserId { get; set; }
	}
}
