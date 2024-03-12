using System;
using System.Collections.Generic;

namespace Review_Film_Project.Models
{
    public partial class Feedback
    {
        public Feedback()
        {
            Replies = new HashSet<Reply>();
        }

        public int Id { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public double? Grade { get; set; }
        public int? MovieId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UserId { get; set; }

        public virtual Movie? Movie { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
