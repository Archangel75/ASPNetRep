using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MyReviewProject.Controllers;
using MyReviewProject.Models;

namespace MyReviewProject.Controllers
{
    [Authorize]
    [Authorize(Roles = "Users")]
    public class ManageController : BaseController
    {
        private ApplicationSignInManager _signInManager;

        public ManageController()
        {
        }
        public ManageController(ApplicationSignInManager signInManager)
        {
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }        

        //
        // GET: /Manage/Index
        [Authorize]
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Ваш пароль изменен."
                : message == ManageMessageId.ChangeLoginSuccess ? "Ваш логин изменен."
                : message == ManageMessageId.EditSucceed ? "Изменения успешно применены."
                : message == ManageMessageId.Error ? "Произошла ошибка."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                userLogin = User.Identity.Name,
                HasPassword = HasPassword(),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }
        

        //
        // GET: /Manage/EditUser
        [HttpGet]
        [Authorize]
        public ActionResult EditUser()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var model = new EditUserViewModel
            {                
                OldLogin = User.Identity.Name,
                OldEmail = UserManager.GetEmail(User.Identity.GetUserId())
            };
            return View(model);
        }

        //
        // POST: /Manage/EditUser
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(EditUserViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }            

            //cast global result for checks.
            IdentityResult result = IdentityResult.Success;
            //pw goes first cause we cant change it with user.
            if (!String.IsNullOrEmpty(model.NewPassword))
            {
                result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (!result.Succeeded)
                    AddErrors(result);
            }

            //pw changed(or not). Lets see about user.
            if (result.Succeeded)
            {
                //We will change all at once, so cast this and user.
                bool needupdate = false;
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                //if email is not null
                if (!String.IsNullOrEmpty(model.NewEmail))
                {
                    if (model.NewEmail.IndexOf('@') > -1)
                    {
                        user.Email = model.NewEmail;
                        needupdate = true;
                    }
                }

                //if login
                if (!String.IsNullOrEmpty(model.NewLogin))
                {
                    user.UserName = model.NewLogin;
                    needupdate = true;
                }

                //if something changed
                if (needupdate)
                {
                    result = await UserManager.UpdateAsync(user);
                    if (!result.Succeeded)
                        AddErrors(result);
                }
            }
            //if we did changed something, then lets head on manage/index with success.
            if (result.Succeeded)
                return RedirectToAction("Index", new { Message = ManageMessageId.EditSucceed });
            return View(model);
        }


        /*
        //
        // GET: /Manage/ChangeLogin
        public ActionResult ChangeLogin()
        {
            var model = new ChangeLoginViewModel
            {
                OldLogin = User.Identity.Name
            };
            return View(model);
        }

        //
        // POST: /Manage/ChangeLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeLogin(ChangeLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                user.UserName = model.NewLogin;
                var result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangeLoginSuccess });
                AddErrors(result);
            }            
            return View(model);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }        
        
    */
        

#region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            EditSucceed,
            ChangeLoginSuccess,
            ChangePasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

#endregion
    }
}