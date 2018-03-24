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
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        
        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }  

       
        public ActionResult Index()
        {
            var users = Db.Users;
            return View(UserManager.Users);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Пользователь не найден" });
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                var validEmail
                    = await UserManager.UserValidator.ValidateAsync(user);

                if (!validEmail.Succeeded)
                {
                    foreach (string error in validEmail.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    validPass
                        = await UserManager.PasswordValidator.ValidateAsync(password);

                    if (validPass.Succeeded)
                    {
                        user.PasswordHash =
                            UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        foreach (string error in validPass.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }
                }

                if ((validEmail.Succeeded && validPass == null) ||
                        (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (string error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }
            return View(user);
        }

    }
}