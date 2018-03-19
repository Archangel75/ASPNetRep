using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyReviewProject.Controllers;
using MyReviewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyReviewProject.Controllers
{
    public class RoleController : BaseController
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
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Index()
        {
            
            var roles = RoleManager.Roles;
            Dictionary<string, string> usersDic = new Dictionary<string, string>();

            foreach (ApplicationRole role in roles)
            {                
                if (role.Users == null || role.Users.Count() == 0)
                {
                    usersDic.Add(role.Name, "Нет пользователей в этой роли");
                }
                else
                {
                    usersDic.Add(role.Name, string.Join(", ", role.Users.Select(x => UserManager.FindById(x.UserId).UserName)));
                }
            }

            ViewBag.UserDic = usersDic;

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
                var result = await RoleManager.CreateAsync(new ApplicationRole
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
                    await RoleManager.DeleteAsync(RoleManager.FindByName(model.Name));
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
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

                return View(new EditRoleModel {Role = role, Description = role.Description, Members = members, NonMembers = nonMembers });
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
                    role.Name = model.RoleName;

                    foreach (string userId in model.IdsToAdd ?? new string[] { })
                    {
                        result = await UserManager.AddToRoleAsync(userId, model.RoleName);

                        if (!result.Succeeded)
                        {
                            foreach (string error in result.Errors)
                            {
                                ModelState.AddModelError("", error);
                            }
                        }
                    }

                    foreach (string userId in model.IdsToDelete ?? new string[] { })
                    {
                        result = await UserManager.RemoveFromRoleAsync(userId,
                        model.RoleName);

                        if (!result.Succeeded)
                        {
                            foreach (string error in result.Errors)
                            {
                                ModelState.AddModelError("", error);
                            }
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