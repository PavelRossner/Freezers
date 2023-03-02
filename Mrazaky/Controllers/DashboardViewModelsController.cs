using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mrazaky.Data;
using Mrazaky.Models;
using Mrazaky.Models.Response;

namespace Mrazaky.Controllers
{
    public class DashboardViewModelsController : Controller
    {
        private readonly ApplicationDbContext db;

        public DashboardViewModelsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: DashboardViewModels
        public IActionResult Index()
        {
            string Id = User.Identity.GetUserId();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id == Id);

            if (user == null)
            {
                return RedirectToAction("Error_user", "Account");
            }

            var foodCount = db.Food.Where(u => u.User.Id == Id).Count();
            var freezerCount = db.Freezer.Where(u => u.User.Id == Id).Count();
            var foodCategoryCount = db.FoodCategory.Where(u => u.User.Id == Id).Count();
            var freezerFoodCount = db.FreezerFood.Where(u => u.User.Id == Id).Select(x => x.FoodID).Distinct().Count();
            var nonexpiredFood = db.Food.Select(FoodResponse.GetFoodResponse).Where(u => u.User.Id == Id).Where(c => c.MonthsToConsume >= 0).Count();
            var expiredFood = db.Food.Select(FoodResponse.GetFoodResponse).Where(u => u.User.Id == Id).Where(c => c.MonthsToConsume < 0).Count();

            db.SaveChanges();

            var model = new DashboardViewModel
            {
                FoodCount = foodCount,
                FreezerCount = freezerCount,
                FoodCategoryCount = foodCategoryCount,
                FreezerFoodCount = freezerFoodCount,
                ExpiredFood = expiredFood,
                NonExpiredFood = nonexpiredFood,
                User = user,
            };

            return View(model);
        }
    }

}
