using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Review_Film_Project.Models;

namespace Review_Film_Project.Pages
{
	public class IndexModel : PageModel
	{

		private readonly review_phim_prn221Context _context;
		public IndexModel(review_phim_prn221Context context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		[BindProperty]
		public Movie NewMovie { get; set; }

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
				return Page();
			}

			var user = JsonSerializer.Deserialize<User>(userSessionJson);

			// Gán thông tin người dùng vào bộ phim
			NewMovie.UserId = user.UserId; // Giả sử Id của người dùng là thuộc tính "Id" trong đối tượng User


			// Thêm bộ phim vào cơ sở dữ liệu
			_context.Movies.Add(NewMovie);
			_context.SaveChanges();

			return RedirectToPage("/Index");
		}

		
		public List<Movie> Movies { get; set; }
		public List<Feedback> Feedbacks { get; set; }
		public int PageIndex { get; set; } = 1;
		public int TotalPages { get; set; }
		public const int PageSize = 5; // Số lượng phim trên mỗi trang

		public void OnGet(int? pageIndex, string searchTerm)
		{
			PageIndex = pageIndex ?? 1;

			IQueryable<Movie> moviesQuery = _context.Movies;

			if (!string.IsNullOrEmpty(searchTerm))
			{
				moviesQuery = moviesQuery.Where(m => m.MovieName.Contains(searchTerm));
			}

			var totalItems = moviesQuery.Count();
			TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

			Movies = moviesQuery
				.Skip((PageIndex - 1) * PageSize)
				.Take(PageSize)
				.ToList();


			Feedbacks = _context.Feedbacks
				.OrderByDescending(f => f.Id) // Sắp xếp theo id
				.Take(8) // Chỉ lấy 10 feedback mới nhất
				.ToList();
		}

		public IActionResult OnPostOnGetLogOut()
		{

			HttpContext.Session.Remove("userSession");
			return RedirectToPage("/Login");
		}
	}
}