using System.ComponentModel.DataAnnotations;

namespace PTPSite.Services
{
	public class ApplicationUser
	{
		public int Id { get; set; }

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
	}
}
