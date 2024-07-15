using Demo.DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        

        #region SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    user = new ApplicationUser()
                    {
                        UserName = model.UserName,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        IsAgree = model.IsAgree
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(SignIn));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                ModelState.AddModelError(string.Empty, "User is already Exist");

            }
            return View(model);
        }


        #endregion

        #region SignIn

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }

                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Login");

            return View(model);
        } 
        #endregion

        #region SignOut
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        #endregion

        #region Forget and Reset Password
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public async Task<IActionResult> SendResetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user= await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        Body = url,
                        Recipient = model.Email
                    };
                    EmailSetting.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Email");


            return View(nameof(ForgetPassword),model);
        }

        public IActionResult CheckYourInbox() {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["Email"] = email;
            TempData["Token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email= TempData["Email"] as string;
                var token= TempData["Token"] as string;
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var result =await _userManager.ResetPasswordAsync(user, token,model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(SignIn));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }


                ModelState.AddModelError(string.Empty, "Invalid Reset Password");

            }
            return View(model);
        }
        #endregion

    }
}
