using System;
using System.Collections.Generic;

namespace Review_Film_Project.Models
{
    public partial class Reply
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? FeedbackId { get; set; }
        public int? RepliedByUserId { get; set; }

        public virtual Feedback? Feedback { get; set; }
        public virtual User? RepliedByUser { get; set; }
    }
}
