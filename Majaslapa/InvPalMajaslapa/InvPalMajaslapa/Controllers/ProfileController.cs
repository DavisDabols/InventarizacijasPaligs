using InvPalMajaslapa.Data;
using InvPalMajaslapa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvPalMajaslapa.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext _db;

        public ProfileController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            return View(user);
        }

        //GET
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(User);
            var model = new EditUserViewModel 
            {
                Name = user.Name,
                Surname = user.Surname,
                CompanyName = user.CompanyName,
                Email = user.Email
            };
            return View(model);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                var updatePassword = await userManager.ChangePasswordAsync(user, obj.OldPassword, obj.Password);
                if (updatePassword.Succeeded)
                {
                    user.Name = obj.Name;
                    user.Surname = obj.Surname;
                    user.CompanyName = obj.CompanyName;
                    user.Email = obj.Email;

                    await userManager.UpdateAsync(user);
                    return RedirectToAction("index", "profile");
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Vecā parole nesakrīt");
                }
            }
            return View(obj);
        }

        //GET
        public async Task<IActionResult> Logs()
        {
            var user = await userManager.GetUserAsync(User);
            IQueryable<Logs> logList = _db.Logs.Where(l => l.UserId == user.Id & l.CreatedDateTime > DateTime.Today.AddDays(-5));
            return View(logList);
        }
    }
}
