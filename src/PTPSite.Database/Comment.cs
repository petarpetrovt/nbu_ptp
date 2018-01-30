using System;
using System.ComponentModel.DataAnnotations;

namespace PTPSite.Database
{
	public class Comment
	{
		public int Id { get; set; }

		[MaxLength(256)]
		public string Text { get; set; }

		public DateTime Date { get; set; }

		public int ByUserId { get; set; }
	}
}
