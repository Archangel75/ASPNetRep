using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace MyReviewProject.Models
{  
    [NotMapped]
    public class CustomReviewDTO
    {
        public int ReviewId { get; set; }

        public DateTime DateCreate { get; set; }

        public string Content { get; set; }
        
        public double Rating { get; set;}

        public byte Recommend { get; set; }

        public byte Exp { get; set; }

        public string Like { get; set; }

        public string Dislike { get; set; }

        public byte[] Image { get; set; }

        //public Image Image { get; set; }

        public string Username { get; set; }

        public string Subjectname { get; set; }
    }

    public class IndexReviewViewModel
    {        
        public IEnumerable<CustomReviewDTO> Reviews { get; set; }
    }

    public class ShowReviewViewModel
    {
        public int ReviewId { get; set; }

        public DateTime DateCreate { get; set; }

        public string Content { get; set; }

        public double Rating { get; set; }

        public byte Recommend { get; set; }

        public byte Exp { get; set; }

        public string Like { get; set; }

        public string Dislike { get; set; }

        public byte[] Image { get; set; }

        public string Username { get; set; }

        public string Subjectname { get; set; }
    }

    public class CreateReviewViewModel
    {
        public Subject ReviewSubject { get; set; }

        public List<Category> Categories { get; set; }

        public List<SubCategory> SubCategories { get; set; }

        [Required]
        public string Objectname { get; set; }
        
        public int SubCategoryId { get; set; }
        public int SubjectId { get; set; }

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