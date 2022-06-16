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
            var warehouseCount = _db.Warehouses.Count(w => w.UserId == user.Id);
            var itemCount = _db.Items.Count(i => i.UserId == user.Id);
            var workerCount = _db.Workers.Count(w => w.UserId == user.Id);
            var totalInventoryValue = _db.Items.Where(i => i.UserId == user.Id).ToList().Aggregate(0m, (current, item) => item.Price * item.Count);
            var viewmodel = new UserStatsViewModel()
            {
                User = user,
                WarehouseCount = warehouseCount,
                ItemCount = itemCount,
                WorkerCount = workerCount,
                TotalInventoryValue = totalInventoryValue
            };
            return View(viewmodel);
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
            IQueryable<Logs> logList = _db.Logs.Where(l => l.UserId == user.Id & l.CreatedDateTime > DateTime.Today.AddDays(-6)).OrderByDescending(l => l.CreatedDateTime);
            return View(logList);
        }
    }
}
