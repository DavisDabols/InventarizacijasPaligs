using InvPalMajaslapa.Data;
using InvPalMajaslapa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvPalMajaslapa.Controllers
{
    public class WorkersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _db;

        public WorkersController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            IQueryable<Worker> objWorkerList = _db.Workers.Where(w => w.UserId == user.Id);
            return View(objWorkerList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Worker obj)
        {
            var user = await userManager.GetUserAsync(User);
            obj.UserId = user.Id;
            obj.Id = Guid.NewGuid();
            obj.Password = BCrypt.Net.BCrypt.HashPassword(obj.Password);
            _db.Workers.Add(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var workerFromDb = _db.Workers.Find(id);
            if (workerFromDb == null)
            {
                return NotFound();
            }
            return View(workerFromDb);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Worker obj)
        {
            var user = await userManager.GetUserAsync(User);
            obj.UserId = user.Id;
            obj.Id = Guid.NewGuid();
            obj.Password = BCrypt.Net.BCrypt.HashPassword(obj.Password);
            _db.Workers.Update(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var workerFromDb = _db.Workers.Find(id);
            if (workerFromDb == null)
            {
                return NotFound();
            }
            return View(workerFromDb);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(Guid? id)
        {
            var obj = _db.Workers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Workers.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

