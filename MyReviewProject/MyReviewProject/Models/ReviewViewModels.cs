using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyReviewProject.Models
{  
    [NotMapped]
    public class CustomReview: Review
    {
        public string Username { get; set; }
    }
    public class IndexReviewViewModel
    {
        
        public IEnumerable<CustomReview> Reviews { get; set; }
    }

    public class CreateReviewViewModel
    {
        public Subject reviewSubject { get; set; }

        public List<Category> Categories { get; set; }

        public List<SubCategory> subCategories { get; set; }

        [Required]
        public string Objectname { get; set; }
        
        public int subCategoryId { get; set; }
        public int subjectId { get; set; }

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