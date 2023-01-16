using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mrazaky.Data;
using Mrazaky.Models;
using Mrazaky.Models.Response;

namespace Mrazaky.Controllers
{
    public class FoodsController : Controller
    {
        private readonly ApplicationDbContext db;

        public FoodsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Foods
        public IActionResult Index()
        {
            string user = User.Identity.GetUserId();

            if (user == null)
            {
                return RedirectToAction("Error", "Foods");
            }

            var applicationDbContext = db.Food.Where(fo => fo.Id == user || User.Identity.Name.Contains("admin")).Include(fo => fo.User)/*.Include(fr => fr.Freezers)*/;
            return View(db.Food.Select(FoodResponse.GetFoodResponse).ToList());
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodId,Id,Category,FoodName,NumberOfPackages,DatePurchase,BestBefore,DaysToConsume,Freezer,FreezerPosition,PackageLabel")] Food food, ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                user = db.User.FirstOrDefault(u => u.Id == this.User.Identity.GetUserId());

                user.Foods.Add(food);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Food == null)
            {
                return NotFound();
            }

            Food food = await db.Food.FindAsync(id);

            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Food food)
        {
            if (id != food.FoodId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(food);
            }

            Food dbFood = db.Food.FirstOrDefault(fo => fo.FoodId == id);

            if (dbFood == null)
            {
                return View(food);
            }

            dbFood.Category = food.Category;
            dbFood.FoodName = food.FoodName;
            dbFood.NumberOfPackages = food.NumberOfPackages;
            dbFood.DatePurchase = food.DatePurchase;
            dbFood.BestBefore = food.BestBefore;
            dbFood.Freezer = food.Freezer;
            dbFood.FreezerPosition = food.FreezerPosition;
            dbFood.PackageLabel = food.PackageLabel;

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        // GET: Foods/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || db.Food == null)
            {
                return NotFound();
            }

            FoodResponse food = db.Food.Include(fo => fo.User)
                                .Select(FoodResponse.GetFoodResponse)
                                .FirstOrDefault(f => f.FoodId == id);

            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Food == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Food'  is null.");
            }
            Food food = await db.Food.FindAsync(id);
            if (food != null)
            {
                db.Food.Remove(food);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return db.Food.Any(e => e.FoodId == id);
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
