using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Review_Film_Project.Models;

namespace Review_Film_Project.Pages
{
	public class RegisterModel : PageModel
	{
		private readonly review_phim_prn221Context _context;
		private readonly IEmailSender _emailSender;
		
		public RegisterModel(review_phim_prn221Context context, IEmailSender emailSender)
		{
			_context = context;
			_emailSender = emailSender;
		}

		[BindProperty]
		public string Name { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		public string Email { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Password is required.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Please confirm your password.")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "The password and rePass do not match.")]
		public string RePassword { get; set; }

		public bool EmailExistsError { get; set; }
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			// Check if password matches re-entered password
			if (Password != RePassword)
			{
				ModelState.AddModelError(string.Empty, "Password and confirm password do not match.");
				return Page();
			}

			// Check if the email is already registered
			var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
			if (existingUser != null)
			{
				EmailExistsError = true;
				return Page();
			}

			// Create a new user
			var newUser = new User
			{
				Name = Name,
				Email = Email,
				Password = Password,
				Active = false,
				// Note: Password should be hashed in a real application for security reasons.
			};

			// Add the user to the database
			_context.Users.Add(newUser);
			await _context.SaveChangesAsync();
			// Tạo mã xác minh duy nhất
			var verificationCode = Guid.NewGuid().ToString();

			// Lưu mã xác minh vào cơ sở dữ liệu
			newUser.CodeActive = verificationCode;
			await _context.SaveChangesAsync();

			// Gửi email xác minh
			var verificationLink = Url.Page(
				"/VerifyAccount",
				pageHandler: null,
				values: new { code = verificationCode },
				protocol: Request.Scheme);

			var emailBody = $"Xin chào {Name},\n\n" +
							$"Vui lòng nhấp vào liên kết sau để xác minh tài khoản của bạn:\n\n" +
							$"{verificationLink}";

			await _emailSender.SendEmailAsync(Email, "Xác minh tài khoản của bạn", emailBody);

			return RedirectToPage("/VerifyAccount");
			// Redirect to a confirmation page or login page
			//return RedirectToPage("/Login");
		}
	}
}
