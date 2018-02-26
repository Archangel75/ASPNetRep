using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyReviewProject.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        public int SubjectId { get; set; }        

        public DateTime DateCreate { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public int Recommend { get; set; }

        public int Exp { get; set; }

        public string Like { get; set; }

        public string Dislike { get; set; }

        public int ImageId { get; set; }

        public byte[] Image { get; set; }
        
    }
}