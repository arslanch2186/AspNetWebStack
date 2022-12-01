using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using SellPhone.Models.DbModels;
using SellPhone.Models.ViewModels;
using SellPhone.Models.ViewModels.ResponseModels;
using SellPhone.Services.Services.Users;
using System;
using System.Threading.Tasks;

namespace SellPhone.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Account/ResetPassword")]
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [Route("/Account/ResetPassword")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);

            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
            {    //RedirectToAction(nameof(ResetPasswordConfirmation));
                ModelState.TryAddModelError("", "User not found(" + resetPasswordModel.Email + ")");
                return View(resetPasswordModel);
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordModel.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(resetPasswordModel);
            }


            return RedirectToAction("ResetPasswordConfirmation", "Account");
        }

        [Route("/Account/ResetPasswordConfirmation")]
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
