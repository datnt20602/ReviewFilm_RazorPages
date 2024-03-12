using System;
using System.Collections.Generic;

namespace Review_Film_Project.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Feedbacks = new HashSet<Feedback>();
            Urls = new HashSet<Url>();
        }

        public int MovieId { get; set; }
        public string? Description { get; set; }
        public string? Director { get; set; }
        public string? Genre { get; set; }
        public string? Image { get; set; }
        public string? MovieName { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public double? AverageGrade { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Url> Urls { get; set; }

		public double CalculateAverageGrade()
		{
			if (Feedbacks == null || Feedbacks.Count == 0)
			{
				// Trả về 0 nếu không có đánh giá
				return 0;
			}

			// Tính tổng số điểm
			double totalGrade = 0;
			foreach (var feedback in Feedbacks)
			{
				totalGrade += feedback.Grade ?? 0; // Cộng thêm điểm của từng đánh giá
			}

			// Tính điểm trung bình
			double averageGrade = totalGrade / Feedbacks.Count;

			// Làm tròn điểm trung bình đến 2 chữ số thập phân
			averageGrade = Math.Round(averageGrade, 2);

			// Gán giá trị điểm trung bình cho thuộc tính của bộ phim và trả về
			this.AverageGrade = averageGrade;
			return averageGrade;
		}
	}
}
