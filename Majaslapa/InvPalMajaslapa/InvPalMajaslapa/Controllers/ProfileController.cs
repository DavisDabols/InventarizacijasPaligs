using InvPalMajaslapa.Data;
using InvPalMajaslapa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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
            var itemCount = _db.Items.Where(i => i.UserId == user.Id).ToList().Aggregate(0, (current, item) => current + item.Count);
            var workerCount = _db.Workers.Count(w => w.UserId == user.Id);
            var totalInventoryValue = _db.Items.Where(i => i.UserId == user.Id).ToList().Aggregate(0m, (current, item) => current + item.Price * item.Count);
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

        public async Task<IActionResult> Csv()
        {
            var user = await userManager.GetUserAsync(User);
            var builder = new StringBuilder();
            builder.AppendLine("Vārds,Uzvārds,Objekta nosaukums,Darbība,Laiks");
            IQueryable<Logs> logs = _db.Logs.Where(l => l.UserId == user.Id).OrderByDescending(l => l.CreatedDateTime);
            foreach (var log in logs)
            {
                builder.AppendLine($"{log.Name},{log.Surname},{log.ItemName},{log.Action},{log.CreatedDateTime}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "notikumi.csv");
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

        //GET
        public IActionResult Delete()
        {
            return View();
        }

        public async Task<IActionResult> DeletePOST()
        {
            var user = await userManager.GetUserAsync(User);
            var logs = _db.Logs.Where(l => l.UserId == user.Id);
            foreach (var log in logs)
            {
                _db.Logs.Remove(log);
            }
            await _db.SaveChangesAsync();
            await signInManager.SignOutAsync();
            var result = userManager.DeleteAsync(user);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
