using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyReviewProject.Models
{
    public class User
    {
        public long Id { get; set; }

        public string UserName { get; set; }
        //public string AspNetId { get; set; }

        //public int AnonUserId { get; set; }
    }

    public class AnonUser
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}