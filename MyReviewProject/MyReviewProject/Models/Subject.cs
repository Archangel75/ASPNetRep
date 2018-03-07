using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyReviewProject.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }

        public string Name { get; set; }

        public int SubCategoryId { get; set; }

        public int AverageRating {
            get { return AverageRating; }
            set
            {
                if (value < 1 || value > 5)
                    value = 4;
                AverageRating = value;

            }
        }


    }
}