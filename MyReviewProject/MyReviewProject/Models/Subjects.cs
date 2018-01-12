using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyReviewProject.Models
{
    public class Subjects
    {
        public int SubjectId { get; set; }

        public string Name { get; set; }

        public string CategoryId { get; set; }

        public int AverageRating { get; set; }


    }
}