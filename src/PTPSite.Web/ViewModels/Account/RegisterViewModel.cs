using System.ComponentModel.DataAnnotations;

namespace PTPSite.Web.ViewModels.Account
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, MinimumLength = 5)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare(nameof(Password), ErrorMessage = "Password is not the same.")]
		public string ConfirmPassword { get; set; }
	}
}
