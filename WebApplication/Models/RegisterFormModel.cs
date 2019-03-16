using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
	public class RegisterFormModel
	{
		public string Username { get; set; }
		public string Password { get; set; }

		public bool IsValid()
		{
			return Username != null &&
			       Password != null;
		}
	}
}
