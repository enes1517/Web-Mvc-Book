using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Repositories;

namespace web.Controllers
{
    public class ProductController : Controller
    {
        private readonly RepositoryContext _context;

        public ProductController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: Ürünleri listele with search and filter
        public IActionResult Index(string searchTitle, string searchAuthor, string filterCategory, double? filterRating)
        {
            var products = _context.Products.AsQueryable();

            // Search by Title
            if (!string.IsNullOrEmpty(searchTitle))
            {
                products = products.Where(p => p.Title.Contains(searchTitle));
            }

            // Search by Author
            if (!string.IsNullOrEmpty(searchAuthor))
            {
                products = products.Where(p => p.Author.Contains(searchAuthor));
            }

            // Filter by Category
            if (!string.IsNullOrEmpty(filterCategory))
            {
                products = products.Where(p => p.Category == filterCategory);
            }

            // Filter by Rating
            if (filterRating.HasValue)
            {
                products = products.Where(p => p.Rating >= filterRating.Value);
            }

            return View(products.ToList());
        }

        // Rest of the existing methods remain unchanged
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Author,Category,Rating")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Author,Category,Rating")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}