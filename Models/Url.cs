using System;
using System.Collections.Generic;

namespace Review_Film_Project.Models
{
    public partial class Url
    {
        public int Id { get; set; }
        public int? MovieId { get; set; }
        public string? UrlValue { get; set; }
        public int? UserId { get; set; }

        public virtual Movie? Movie { get; set; }
        public virtual User? User { get; set; }
    }
}
