using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarTime.Domain;
using CarTime.Domain.Entities;
using CarTime.Models;
using CarTime.Service;

namespace CarTime.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<UserData> userManager;
        private readonly SignInManager<UserData> signInManager;
        private readonly IWebHostEnvironment _hostingEnv;

        private readonly AppDbContext context;
        private readonly ShopCart _shopCart;

        public AccountController(UserManager<UserData> userMgr, SignInManager<UserData> signinMgr,
            IWebHostEnvironment _hostingEnv, AppDbContext context, ShopCart shopCart)
        {
            userManager = userMgr;
            signInManager = signinMgr;
            this._hostingEnv = _hostingEnv;
            this.context = context;
            _shopCart = shopCart;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new LoginViewModel());
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserData user = await userManager.FindByNameAsync(model.UserName);
                if(HttpContext.Session.GetString("ShopCartId") == null)
                {
                    HttpContext.Session.SetString("ShopCartId", Guid.NewGuid().ToString());
                    var userData = context.UserData.FirstOrDefault(u => u.UserName == model.UserName);
                    _shopCart.UserDataId = userData.Id;
                    _shopCart.UserData = userData;
                }
                if (user != null)
                {
                    HttpContext.Session.SetString("UserId", user.Id);
                    IQueryable<IdentityUserRole<string>> role = context.UserRoles;
                    IdentityUserRole<string> foruser = new IdentityUserRole<string>();
                    foreach (IdentityUserRole<string> userRole in role)
                    {
                        if (user.Id == userRole.UserId)
                        {
                            foruser = userRole;
                        }
                    }
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (foruser.UserId == user.Id && foruser.RoleId == "e3ee9b61-b13d-409f-bcd6-cc0210b82cbd")
                            return Redirect(returnUrl ?? "/Manager/EditMain");
                        else if (foruser.UserId == user.Id && foruser.RoleId == "91f88ad6-fbdf-432e-a5cb-828e97def61d")
                            return Redirect(returnUrl ?? "/Admin/Manage");
                        else if (foruser.UserId == user.Id && foruser.RoleId == "26CB2F53-B690-4854-84F1-28DF22C44C28")
                            return Redirect(returnUrl ?? "/User/UserMain");
                        else
                        {
                            return Redirect(returnUrl ?? "/Cars");
                        }
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверный логин или пароль.");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            HttpContext.Session.SetString("ShopCartId", Guid.NewGuid().ToString());
            HttpContext.Session.Set<UserData>("RentalUserData", null);
            return RedirectToAction("Index", "Main");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            UserData model = new UserData();
            ViewBag.Except = false;
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserData modell)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await userManager.CreateAsync(modell, modell.PasswordHash);
                    if (result.Succeeded)
                    {
                        var res = await userManager.AddToRoleAsync(modell, "user");
                        if (res.Succeeded)
                        {
                            return RedirectToAction(nameof(AccountController.Login),
                            nameof(AccountController).CutController());
                        }
                    }
                    else
                    {
                        ViewBag.Except = true;
                        return View(modell);
                    }
                }
                catch (DbUpdateException e)
                {
                    ViewBag.Except = true;
                    return View(modell);
                }

            }
            return View(modell);
        }
    }
}