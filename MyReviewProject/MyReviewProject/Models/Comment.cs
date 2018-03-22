using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyReviewProject.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime EditTime { get; set; }

        public int ReplyToId { get; set; }

    }
}