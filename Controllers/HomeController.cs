using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Repositories;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        private readonly RepositoryContext _context;

        public HomeController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: Ürünleri listele
        public IActionResult Index(string searchTitle, string searchAuthor, string filterCategory, double? filterRating)
        {
            var products = _context.Products
                .Include(p => p.Comments)  // Include comments for each product
                .AsQueryable();

            // Search by Title
            if (!string.IsNullOrEmpty(searchTitle))
            {
                products = products.Where(p => p.Title.Contains(searchTitle));
                ViewData["searchTitle"] = searchTitle;
            }

            // Search by Author
            if (!string.IsNullOrEmpty(searchAuthor))
            {
                products = products.Where(p => p.Author.Contains(searchAuthor));
                ViewData["searchAuthor"] = searchAuthor;
            }

            // Filter by Category
            if (!string.IsNullOrEmpty(filterCategory))
            {
                products = products.Where(p => p.Category == filterCategory);
                ViewData["filterCategory"] = filterCategory;
            }

            // Filter by Rating
            if (filterRating.HasValue)
            {
                products = products.Where(p => p.Rating >= filterRating.Value);
                ViewData["filterRating"] = filterRating;
            }

            return View(products.ToList());
        }

        // POST: Add a comment to a product
        [HttpPost]
        public IActionResult AddComment(int productId, string commentText)
        {
            if (string.IsNullOrEmpty(commentText))
            {
                TempData["ErrorMessage"] = "Yorum alaný boþ olamaz.";
                return RedirectToAction("Index");
            }

            var product = _context.Products
                .FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return NotFound();
            }

            // Add new comment
            var comment = new Comment
            {
                Text = commentText,
                ProductId = productId,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Yorumunuz baþarýyla eklendi.";
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}