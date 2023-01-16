using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mrazaky.Data;
using Mrazaky.Models;

namespace Mrazaky.Controllers
{
    public class AccountController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext db;

        public AccountController                                                     //Konstruktor
        (
            Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager,  //Instance tříd UserManager a SignInManager jsou parametry konstruktoru; získají se mechanismem Dependency Injection - do kontroleru se pošlou samy
            SignInManager<ApplicationUser> signInManager,                            //To se zajistí registrací služeb v Program.cs , vlastností builder.Services
            ApplicationDbContext db
        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.db = db;
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }


        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };

                db.SaveChanges();

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToLocal(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string navratovaURL = null)
        {
            ViewData["ReturnUrl"] = navratovaURL;
            if (ModelState.IsValid)
            {
                var vysledekOvereni = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (vysledekOvereni.Succeeded)
                {
                    return RedirectToLocal(navratovaURL);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Neplatné přihlašovací údaje.");
                    return View(model);
                }
            }

            // Pokud byly odeslány neplatné údaje, vrátíme uživatele k přihlašovacímu formuláři
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Index()
        {
            //return View(db.Users.ToList());

            var user = User.Identity.GetUserId();

            if (user == null)
            {
                return RedirectToAction("Error_user", "Account");
            }

            else if (User.Identity.Name.Contains("admin"))
            {
                return View(db.Users.ToList());
            }

            else
            {
                return RedirectToAction("Error_admin", "Account");
            }
        }


        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id, string FoodName, string FreezerLocation)
        {
            if (id == null || db.Users == null)
            {
                return NotFound();
            }

            var food = db.Food.Include(fi => fi.FoodName);
            var freezer = db.Freezer.Include(fr => fr.FreezerLocation);
            if (food != null || freezer != null)

            { return RedirectToAction("Error_database", "Account"); }

            var user = await db.Users
            .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (db.Users == null)
            {
                return Problem("Entity set 'ApplicationDbContext.User'  is null.");
            }

            var user = await db.Users.FindAsync(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error_admin()

        {
            return View();
        }

        public IActionResult Error_user()

        {
            return View();
        }

        public IActionResult Error_database()

        { return View(); }
    }

}

