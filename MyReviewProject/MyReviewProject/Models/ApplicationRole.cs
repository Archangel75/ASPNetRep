using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyReviewProject.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(): base() { }

        public string Description { get; set; }
    }

    public class EditRoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        public string Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateRoleModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}