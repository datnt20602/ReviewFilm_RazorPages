using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Review_Film_Project.Models;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Review_Film_Project.Pages
{
	public class LoginModel : PageModel
	{
		private readonly review_phim_prn221Context _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		[BindProperty]
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[BindProperty]
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public LoginModel(review_phim_prn221Context context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public IActionResult OnPost()
		{
			var user = _context.Users.FirstOrDefault(u => u.Email == Email);

			if (user == null)
			{
				// Tài khoản không tồn tại
				ModelState.AddModelError(string.Empty, "Not Found Your Account.");
				return Page();
			}

			if (user.Active == false)
			{
				// Tài khoản chưa được kích hoạt
				ModelState.AddModelError(string.Empty, "Chưa active tài khoản, Vui lòng kiểm tra mail.");
				return Page();
			}

			// Kiểm tra mật khẩu
			if (user.Password != Password)
			{
				// Sai mật khẩu
				ModelState.AddModelError(string.Empty, "Wrong Password.");
				return Page();
			}

			// Đăng nhập thành công
			// Chuyển đổi đối tượng thành chuỗi JSON
			string userJson = JsonSerializer.Serialize(user);

			// Lưu chuỗi JSON vào Session
			_httpContextAccessor.HttpContext.Session.SetString("userSession", userJson);

			return RedirectToPage("/Index"); // Điều hướng đến trang chính sau khi đăng nhập
		}
	}
}
