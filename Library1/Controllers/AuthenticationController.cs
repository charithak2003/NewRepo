using Library1.Data;
using Library1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Library1.Controllers
{
    public class LoginPageController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginPageController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Attempt to sign in the user with the decoded password
                    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetAllBranches", "Branch");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Invalid password format.");
                }
            }
            return View(model);
        }


    }
}
