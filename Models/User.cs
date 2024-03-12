using System;
using System.Collections.Generic;

namespace Review_Film_Project.Models
{
    public partial class User
    {
        public User()
        {
            Feedbacks = new HashSet<Feedback>();
            Movies = new HashSet<Movie>();
            Replies = new HashSet<Reply>();
            Urls = new HashSet<Url>();
        }

        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public bool Active { get; set; }
        public string? CodeActive { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Url> Urls { get; set; }
    }
}
