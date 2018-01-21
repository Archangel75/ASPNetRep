using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyReviewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyReviewProject.Controllers
{
    public class RoleController : Controller
    {

        private ApplicationUserManager _userManager;

        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new ApplicationRole
                {
                    Name = model.Name,
                    Description = model.Description
                });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                string[] memberIDs = role.Users.Select(x => x.UserId).ToArray();

                IEnumerable<ApplicationUser> members
                = UserManager.Users.Where(x => memberIDs.Any(y => y == x.Id));

                IEnumerable<ApplicationUser> nonMembers = UserManager.Users.Except(members);

                return View(new EditRoleModel { Id = role.Id, Name = role.Name, Description = role.Description, Members = members, NonMembers = nonMembers });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleModificationModel model)
        {            
            if (ModelState.IsValid)
            {
                IdentityResult result;
                ApplicationRole role = await RoleManager.FindByIdAsync(model.Id);               

                if (role != null)
                { 
                    role.Description = model.Description;
                    role.Name = model.Name;

                    foreach (string userId in model.IdsToAdd ?? new string[] { })
                    {
                        result = await UserManager.AddToRoleAsync(userId, model.RoleName);

                        if (!result.Succeeded)
                        {
                            return View("Error", result.Errors);
                        }
                    }

                    foreach (string userId in model.IdsToDelete ?? new string[] { })
                    {
                        result = await UserManager.RemoveFromRoleAsync(userId,
                        model.RoleName);

                        if (!result.Succeeded)
                        {
                            return View("Error", result.Errors);
                        }
                    }
                    return RedirectToAction("Index");
                    
                }
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }


    }
}