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

        private double _rating;
        public double Rating {
            get { return _rating; }
            set {
                if (value < 1 || value > 5)
                    value = 4;
                _rating = value;
                    
            }
        }

        public int Recommend { get; set; }

        public int Exp { get; set; }

        public string Like { get; set; }

        public string Dislike { get; set; }

        public int ImageId { get; set; }

        public byte[] Image { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser User { get; private set; }
        
    }
}