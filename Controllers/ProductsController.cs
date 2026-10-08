using Microsoft.AspNetCore.Mvc;
using Averilla_Midterm_Store.Data;
using Averilla_Midterm_Store.Models;

namespace Averilla_Midterm_Store.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var products = _db.Products.ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);

            if (product == null)
                return RedirectToAction("Index");

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);

            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
