﻿using System.ComponentModel.DataAnnotations;

namespace PTPSite.Web.Models.AccountViewModels
{
	public class LoginViewModel
	{
		[EmailAddress]
		[Display(Name = "Email")]
		[Required]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		[Required]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}
}
