using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyReviewProject.Models
{
    public class IndexReviewViewModel
    {
        public ReviewContext Review { get; set; }

        public List<Category> Categories { get; set; }

        public List<SubCategory> subCategories { get; set; }


    }

    public class CreateReviewViewModel
    {
        public List<Category> Categories { get; set; }

        public List<SubCategory> subCategories { get; set; }

        [Required]
        public string Objectname { get; set; }

        [Required]
        public string Category { get; set; }
        [Required]
        public string subCategory { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public bool Recomendations { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Пожалуйста, изложите плюсы вкратце.")]
        public string Like { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Пожалуйста, изложите минусы вкратце.")]
        public string Dislike { get; set; }

        [Required]
        public string Comment { get; set; }

        public byte[] Image { get; set; }
    }
}