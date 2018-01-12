using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyReviewProject.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public string SubjectName { get; set; }

        public string Author { get; set; }

        public DateTime DateCreate { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public int CategoryId { get; set; }
    }
}