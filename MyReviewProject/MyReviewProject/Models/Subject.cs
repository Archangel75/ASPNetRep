﻿using System;
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

        public int AverageRating { get; set; }


    }
}