using Microsoft.AspNetCore.Mvc;
using web.Models;
using web;
using web.Repositories;


// UserController.cs
namespace web.Controllers
{
    public class UserController : Controller
    {
        private readonly RepositoryContext _Context;

        public UserController(RepositoryContext context)
        {
            _Context = context;
        }

        public IActionResult Index()
        {
            return View(_Context.User.ToList());
        }

        public IActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Apply([FromForm] Users user)
        {
            if (_Context.User.Any(c => c.Email.Equals(user.Email)))
            {
                ModelState.AddModelError("", "there is already member");
            }

            if (ModelState.IsValid)
            {
                _Context.User.Add(user);
                _Context.SaveChanges();
                return View("feedback", user);
            }
            else
            {
                return View();
            }
        }

        // New Delete action methods
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _Context.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _Context.User.Find(id);
            if (user != null)
            {
                _Context.User.Remove(user);
                _Context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
