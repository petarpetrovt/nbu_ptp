using System;
using System.ComponentModel.DataAnnotations;

namespace PTPSite.Database
{
	public class Comment
	{
		public string Id { get; set; }

		[MaxLength(256)]
		public string Text { get; set; }

		public DateTime Date { get; set; }

		public string ByUserId { get; set; }

		public Comment()
		{
			Id = Guid.NewGuid().ToString();
		}
	}
}
