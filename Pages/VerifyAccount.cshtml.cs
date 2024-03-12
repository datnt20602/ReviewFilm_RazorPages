using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Review_Film_Project.Models;

namespace Review_Film_Project.Pages
{
    public class VerifyAccountModel : PageModel
    {
		private readonly review_phim_prn221Context _context;

		public VerifyAccountModel(review_phim_prn221Context context)
		{
			_context = context;
		}

		public async Task<IActionResult> OnGetAsync(string code)
		{
			if (string.IsNullOrEmpty(code))
			{
				return RedirectToPage("/Index");
			}

			var user = await _context.Users.FirstOrDefaultAsync(u => u.CodeActive == code);
			if (user == null)
			{
				return NotFound($"Không tìm thấy người dùng với mã xác minh '{code}'.");
			}

			// Cập nhật trạng thái tài khoản thành đã kích hoạt
			user.Active = true;
			

			await _context.SaveChangesAsync();

			return RedirectToPage("/Login"); // Chuyển hướng đến trang đăng nhập
		}
	}
}
