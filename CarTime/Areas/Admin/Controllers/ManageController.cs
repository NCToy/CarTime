using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration;
using CarTime.Domain;
using CarTime.Models;
using CarTime.Service;

namespace CarTime.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageController : Controller
    {
        private readonly UserManager<UserData> userManager;
        private readonly SignInManager<UserData> signInManager;
        private readonly AppDbContext context;
        private readonly DataManager dataManager;

        public ManageController(UserManager<UserData> userMgr, SignInManager<UserData> signinMgr, AppDbContext context, DataManager dataManager)
        {
            userManager = userMgr;
            signInManager = signinMgr;
            this.context = context;
            this.dataManager = dataManager;
        }

        public IActionResult Index()
        {
            IQueryable<IdentityUserRole<string>> role =
                context.UserRoles.Where(r => r.RoleId == "e3ee9b61-b13d-409f-bcd6-cc0210b82cbd");
            List<UserData> userData = new List<UserData>();
            foreach (IdentityUserRole<string> user in role)
            {
                UserData manager = context.UserData.FirstOrDefault(u => u.Id == user.UserId);
                if (manager != null)
                   userData.Add(manager);
            }
            return View(userData);
        }

        public IActionResult Edit(UserData data)
        {
            ViewBag.Except = false;
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(UserData data, string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.UserData.Update(data);
                    context.SaveChanges();
                    return RedirectToAction(nameof(ManageController.Index), nameof(ManageController).CutController());
                }
                catch (DbUpdateException e)
                {
                    ViewBag.Except = true;
                    return View(data);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(UserData data)
        {
            context.UserData.Remove(data);
            return RedirectToAction(nameof(ManageController.Index), nameof(ManageController).CutController());
        }

        public IActionResult Register()
        {
            RegisterViewModel data = new RegisterViewModel();
            ViewBag.Except = false;
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel modell)
        {
            if (ModelState.IsValid)
            {
                UserData user = new UserData();
                user.Id = Guid.NewGuid().ToString();
                user.UserName = modell.UserName;
                user.PasswordHash = modell.Password;
                user.Name = modell.Name;
                user.Surname = modell.Surname;
                user.Patronymic = modell.Patronymic;
                user.Email = modell.Email;
                user.PhoneNumber = modell.PhoneNumber;
                user.DateAdded = DateTime.UtcNow;

                var result = await userManager.CreateAsync(user, modell.Password);
                if (result.Succeeded)
                {
                    var resultt = await userManager.AddToRoleAsync(user, "manager");
                    if (resultt.Succeeded)
                    {
                        return RedirectToAction(nameof(ManageController.Index),
                        nameof(ManageController).CutController());
                    }
                    
                }
            }

            return View(modell);
        }
    }
}
