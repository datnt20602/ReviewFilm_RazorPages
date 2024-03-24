using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Review_Film_Project.Models;

namespace Review_Film_Project.Pages
{
    public class ChangePasswordModel : PageModel
    {
		private readonly review_phim_prn221Context _context;
		public ChangePasswordModel(review_phim_prn221Context context)
		{
			_context = context;
		}
		
		[BindProperty]
		[Required(ErrorMessage = "Password is required.")]
		[DataType(DataType.Password)]
		public string OldPassword { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Password is required.")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Please confirm your password.")]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "Nhập lại mật khẩu không khớp.")]
		public string ReNewPassword { get; set; }

		public void OnGet()
        {
        }

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			// Truy cập thông tin người dùng từ Session
			var userSessionJson = HttpContext.Session.GetString("userSession");
			if (string.IsNullOrEmpty(userSessionJson))
			{
				// Xử lý trường hợp không có thông tin người dùng từ Session
				return RedirectToPage("/Login"); ;
			}
			var user = JsonSerializer.Deserialize<User>(userSessionJson);


			var userFromDb = _context.Users.FirstOrDefault(u => u.Email == user.Email);
			if (userFromDb == null)
			{
				return Page(); // Xử lý trường hợp không tìm thấy người dùng trong database
			}

			// So sánh mật khẩu cũ
			if (userFromDb.Password != OldPassword)
			{
				ModelState.AddModelError(string.Empty, "Mật khẩu cũ không đúng.");
				return Page(); // Trả về trang với thông báo lỗi
			}

			//So sánh 2 mật khẩu mới

			// Cập nhật mật khẩu mới
			userFromDb.Password = NewPassword;
			_context.SaveChanges();

			return RedirectToPage("/Index"); // Chuyển hướng về trang chính sau khi đổi mật khẩu thành công
		}

			
		
    }
}
