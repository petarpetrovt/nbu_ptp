using System;
using System.ComponentModel.DataAnnotations;

namespace PTPSite.Database
{
	public class ApplicationUser
	{
		public string Id { get; set; }

		public string PasswordHash { get; set; }

		[MaxLength(256)]
		public string Email { get; set; }

		[MaxLength(256)]
		public string UserName { get; set; }

		[MaxLength(256)]
		public string NormalizedUserName { get; set; }

		[MaxLength(256)]
		public string Name { get; set; }

		public ApplicationRole Role { get; set; }

		public ApplicationUser()
		{
			Id = Guid.NewGuid().ToString();
		}
	}
}
