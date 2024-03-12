using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Review_Film_Project.Models;

namespace Review_Film_Project.Pages
{
	public class Movie_detailModel : PageModel
	{

		private readonly review_phim_prn221Context _context; // Thay YourDbContext bằng DbContext của bạn

		public Movie_detailModel(review_phim_prn221Context context)
		{
			_context = context;
		}

		public Movie Movie { get; set; }
		public Url url { get; set; }
		public List<Feedback> Feedbacks { get; set; }
		[BindProperty]
		public Feedback newFeedback { get; set; }
		[BindProperty]
		public Url newUrl { get; set; }

		[BindProperty]
		public Reply NewReply { get; set; }

		public int PageIndex { get; set; } = 1;
		public int TotalPages { get; set; }
		public const int PageSize = 3; // Số lượng phim trên mỗi trang


		public async Task<IActionResult> OnGetAsync(int id, int? pageIndex)
		{
			Movie = await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == id);
			url = await _context.Urls.FirstOrDefaultAsync(u => u.MovieId == id);

			PageIndex = pageIndex ?? 1;
			var feedback = await _context.Feedbacks
							   .Include(f => f.Replies) // Load các reply tương ứng với mỗi feedback
							   .FirstOrDefaultAsync(f => f.MovieId == id);
			IQueryable<Feedback> feedbackQuery = _context.Feedbacks.Include(f=>f.Replies)
				.ThenInclude(r => r.RepliedByUser).Where(f => f.MovieId == id).OrderByDescending(f=>f.Id);

			var totalItems = feedbackQuery.Count();
			TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

			Feedbacks = feedbackQuery
				.Skip((PageIndex - 1) * PageSize)
				.Take(PageSize)
				.ToList();

			if (Movie == null)
			{
				return NotFound();
			}


			// Load các phản hồi liên quan đến phim
			Movie.Feedbacks = await _context.Feedbacks.Where(f => f.MovieId == id).Include(f=>f.Replies).
				ThenInclude(r => r.RepliedByUser).ToListAsync();

			// Tính toán điểm trung bình của phim
			Movie.CalculateAverageGrade();
			//Cập nhật điểm trung bình của phim vào db
			_context.Movies.Update(Movie);
			await _context.SaveChangesAsync();


			// Truyền dữ liệu sang view sử dụng ViewData
			ViewData["MovieImg"] = Movie.Image;
			ViewData["MovieName"] = Movie.MovieName;
			ViewData["Description"] = Movie.Description;
			ViewData["Director"] = Movie.Director;
			ViewData["Genre"] = Movie.Genre;
			ViewData["ReleaseDate"] = Movie.ReleaseDate;
			ViewData["AverageGrade"] = Movie.AverageGrade;

			ViewData["Urls"] = await _context.Urls
			.Where(u => u.MovieId == id)
			.Select(u => u.UrlValue)
			.ToListAsync();



			return Page();
		}

		public IActionResult OnPost(int id)
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

			// Gán thông tin người dùng vào 
			newFeedback.UserId = user.UserId; // Giả sử Id của người dùng là thuộc tính "Id" trong đối tượng User
			newFeedback.MovieId = id;
			newFeedback.CreatedAt = DateTime.Now;

			// Thêm bộ phim vào cơ sở dữ liệu
			_context.Add(newFeedback);
			_context.SaveChanges();

			return RedirectToPage("/Movie-detail", new { id = id });
		}

		public async Task<IActionResult> OnPostAddUrlAsync(int id)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var userSessionJson = HttpContext.Session.GetString("userSession");
			if (string.IsNullOrEmpty(userSessionJson))
			{
				return Page();
			}

			var user = JsonSerializer.Deserialize<User>(userSessionJson);

			newUrl.UserId = user.UserId;
			newUrl.MovieId = id;

			// Thêm đối tượng url vào cơ sở dữ liệu
			_context.Urls.Add(newUrl); // Sử dụng context của Entity Framework để thêm
			await _context.SaveChangesAsync();

			return RedirectToPage("/Movie-detail", new { id = id });
		}
		public async Task<IActionResult> OnPostReplyAsync(int id)
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var userSessionJson = HttpContext.Session.GetString("userSession");
			if (string.IsNullOrEmpty(userSessionJson))
			{
				// Xử lý trường hợp không có thông tin người dùng từ Session
				return Page();
			}

			var user = JsonSerializer.Deserialize<User>(userSessionJson);
			var feedbackId = Request.Form["feedbackId"];
			NewReply.RepliedByUserId = user.UserId;
			NewReply.FeedbackId = int.Parse(feedbackId);
			NewReply.CreatedAt = DateTime.Now;

			_context.Replies.Add(NewReply);
			await _context.SaveChangesAsync();

			return RedirectToPage("/Movie-detail", new { id = id });
		}
	}

}


