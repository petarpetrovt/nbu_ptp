using System.ComponentModel.DataAnnotations;

namespace PTPSite.Web.Models.AccountViewModels
{
	public class LoginViewModel
	{
		[EmailAddress]
		[Display]
		[Required]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Display]
		[Required]
		public string Password { get; set; }

		[Display]
		public bool RememberMe { get; set; }
	}
}
